using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using VeloTours.Models;

namespace VeloTours.ViewModels
{
    public abstract class RideViewModel
    {
        public enum RideType { Area, Region };
        protected RideType rideType;

        #region Module

        public Nullable<double> KomSpeed { get; set; }
        public AthleteRideInfo Athlete { get; set; }

        public IPagedList<Models.LeaderBoard> LeaderBoard { get; set; }

        public LeaderBoard YellowYersey { get; set; }
        public LeaderBoard GreenYersey { get; set; }
        public LeaderBoard PolkaDotYersey { get; set; }

        public virtual Statistics Info { get; private set; }
        public virtual int RideID { get; private set; }
        
        public virtual int AreaType { get { return (int)SegmentArea.AreaTypeEnum.Default; }  }

        #endregion

        #region

        protected Models.LeaderBoard lBoardAthlete = null;
        public virtual IEnumerable<RideViewModel> RideList { get; protected set; }

        #endregion

        public String KomSpeedString { get { return (KomSpeed == null) ? null : String.Format("{0:#0.0}", KomSpeed); } }
        public double CalculateSpeed(Models.LeaderBoard leaderBoard) { return (this.Info.Distance / leaderBoard.ElapsedTimes.Min) * 3.6; }

        //private string DefaultLBoardSortOrder = "Min";
        private static int pageSize = 20;
        //string lbSortOrder = null;


        protected bool SetRide(int athleteID, int? leaderBoardPageNo, Result dbResult)
        {
            SetYersey(dbResult);

            bool hasLeaderBoard = LeaderBoardExists(dbResult);
            if (hasLeaderBoard)
            {
                var leaderBoards = dbResult.LeaderBoards;
                var lBoardKOM = leaderBoards.OrderBy(x => x.ElapsedTimes.Min).First();
                lBoardAthlete = leaderBoards.SingleOrDefault(x => x.AthleteID == athleteID);

                Athlete = AddAthleteToViewModel(athleteID, leaderBoards, lBoardKOM, lBoardAthlete, Info.NoRiders);
                KomSpeed = Info.SpeedInKmH(lBoardKOM);

                if (leaderBoardPageNo > 0)
                {
                    LeaderBoard = GetPagedLeaderBoards(leaderBoards, leaderBoardPageNo);
                }
            }
            else
            {
                LeaderBoard = CreateBlankPagedList();
                Athlete = CreateBlankAthleteInfo();
            }
            return hasLeaderBoard;
        }

        protected void SetYersey(Result dbResult)
        {
            YellowYersey = (dbResult != null) ? dbResult.YellowYerseyLB : null;
            GreenYersey = (dbResult != null) ? dbResult.GreenYerseyLB : null;
            PolkaDotYersey = (dbResult != null) ? dbResult.PolkaDotYerseyLB : null;
        }

        internal IPagedList<Models.LeaderBoard> GetPagedLeaderBoards(ICollection<LeaderBoard> leaderBoard, int? lBoardPage)
        {
            // TODO: Add where to filter where bigger than one for segments
            IOrderedEnumerable<Models.LeaderBoard> sortedlBoard = null;
            //if(rideType == RideType.Region)
            //    sortedlBoard = leaderBoard.Where(x => x.NoRidden > 1).OrderBy(s => s.YellowPoints);
            //else
                sortedlBoard = leaderBoard.OrderBy(s => s.YellowPoints);

            UpdateRankForCustomSortedLeaderBoards(ref sortedlBoard);

            return sortedlBoard.ToPagedList(lBoardPage ?? 1, pageSize);
        }

        internal static AthleteRideInfo AddAthleteToViewModel(int athleteID, ICollection<LeaderBoard> leaderBoards, Models.LeaderBoard lBoardKOM, Models.LeaderBoard lBoardAthlete, int noRiders)
        {
            AthleteRideInfo athleteRideInfo = new AthleteRideInfo();
            if (lBoardAthlete != null)
            {
                athleteRideInfo.BehindKom = lBoardAthlete.ElapsedTimes.Min - lBoardKOM.ElapsedTimes.Min;
                athleteRideInfo.BehindKomPercentage = ((double)lBoardAthlete.ElapsedTimes.Min / (double)lBoardKOM.ElapsedTimes.Min - 1) * 100;
                athleteRideInfo.ElapsedTimes = lBoardAthlete.ElapsedTimes;
                athleteRideInfo.NoRidden = lBoardAthlete.NoRidden;
                athleteRideInfo.Position = lBoardAthlete.Rank;
                athleteRideInfo.PositionPercentage = ((double)lBoardAthlete.Rank / (double)noRiders * 100);

                athleteRideInfo.Green = lBoardAthlete.GreenPoints;
                athleteRideInfo.PolkaDot = lBoardAthlete.PolkaDotPoints;
            }
            return athleteRideInfo;
        }

        internal static bool LeaderBoardExists(Result dbResult)
        {
            return dbResult != null && dbResult.LeaderBoards != null && dbResult.LeaderBoards.Count > 0;
        }

        private static void UpdateRankForCustomSortedLeaderBoards(ref IOrderedEnumerable<LeaderBoard> sortedlBoard)
        {
            // TODO: Fix rank if same as previous row.
            int rank = 0;
            foreach (var item in sortedlBoard)
            {
                item.Rank = ++rank;
            }
        }

        internal static IPagedList<LeaderBoard> CreateBlankPagedList()
        {
            return new List<Models.LeaderBoard>().ToPagedList(1, 1);
        }

        internal static AthleteRideInfo CreateBlankAthleteInfo()
        {
            return new AthleteRideInfo();
        }

        #region //private IPagedList<Models.LeaderBoard> GetSortedAndPagedLeaderBoards(int resultID, string sortBy)
        //{
        //    var lBoard =
        //        from l in db.LeaderBoards
        //        where l.ResultID == resultID
        //        select l;

        //    bool isSameField = LeaderBoardSortOrder != null && sortBy.Equals(GetSortField(LeaderBoardSortOrder));
        //    bool isAscending = !sortBy.Equals(LeaderBoardSortOrder);

        //    LeaderBoardSortOrder = sortBy ?? DefaultLBoardSortOrder;
        //    if (isSameField && isAscending)
        //        LeaderBoardSortOrder += " desc";

        //    IQueryable<Models.LeaderBoard> sortedlBoard = null;
        //    switch(GetSortField(LeaderBoardSortOrder))
        //    {
        //        case "Name":
        //        default:
        //            sortedlBoard = (isAscending) ? lBoard.OrderBy(s => s.Athlete.Name) : lBoard.OrderByDescending(s => s.Athlete.Name);
        //            break;
        //        case "Min":
        //            sortedlBoard = (isAscending) ? lBoard.OrderBy(s => s.ElapsedTimes.Min) : lBoard.OrderByDescending(s => s.ElapsedTimes.Min) ;
        //            break;
        //        case "Avg":
        //            sortedlBoard = (isAscending) ? lBoard.OrderBy(s => s.ElapsedTimes.Average) : lBoard.OrderByDescending(s => s.ElapsedTimes.Average);
        //            ModelUtils.UpdateRankForCustomSortedLeaderBoards(ref sortedlBoard);
        //            break;
        //        //...
        //    }

        //    //UpdateRankForCustomSortedLeaderBoards(ref sortedlBoard);

        //    return sortedlBoard.ToPagedList(lbPage ?? 1, pageSize);
        //}

        //private string GetSortField(string sortBy)
        //{
        //    int pos = sortBy.IndexOf(" ");
        //    if (pos > 0)
        //    {
        //        return sortBy.Substring(0, pos);
        //    }
        //    return sortBy;
        //}
        #endregion
    }
}
