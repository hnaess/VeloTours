using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using VeloTours.Models;

namespace VeloTours.ViewModels
{
    public class SegmentViewModel
    {
        #region Properties

        public Models.Segment Segment { get; set; }
        public Nullable<double> KomSpeed { get; set; }
        public AthleteRideInfo Athlete { get; set; }

        public string ImprovementHint { get; set; }

        public Statistics Info { get { return Segment.Info; } }
        public IPagedList<Models.LeaderBoard> LeaderBoard { get; set; }

        public LeaderBoard YellowYersey { get; set; }
        public LeaderBoard GreenYersey { get; set; }
        public LeaderBoard PolkaDotYersey { get; set; }

        public ICollection<SegmentArea> SegmentAreas { get; set; }

        public double CalculateSpeed(Models.LeaderBoard leaderBoard)
        {
            return (this.Segment.Info.Distance / leaderBoard.ElapsedTimes.Min) * 3.6;
        }

        public bool IsClimb { get { return IsClimbCat(Info.ClimbCategory); } }

        public static bool IsClimbCat(string climbCategory)
        {
            return climbCategory != "NC";
        }

        public String KomSpeedString { get { return (KomSpeed == null) ? null : String.Format("{0:#0.0}", KomSpeed); } }
        
        #endregion
        
        #region Constructors

        public SegmentViewModel(int athleteID, Models.Segment dbSegment, int? leaderBoardPageNo)
        {
            var dbResult = dbSegment.Result;

            Segment = dbSegment;
            SegmentAreas = dbSegment.SegmentAreas;

            SetYersey(dbResult);

            if (RideUtil.LeaderBoardExists(dbResult))
            {
                var leaderBoards = dbResult.LeaderBoards;
                var lBoardKOM = leaderBoards.OrderBy(x => x.ElapsedTimes.Min).First();
                var lBoardAthlete = leaderBoards.SingleOrDefault(x => x.AthleteID == athleteID);

                KomSpeed = Info.SpeedInKmH(lBoardKOM);
                Athlete = RideUtil.AddAthleteToViewModel(athleteID, leaderBoards, lBoardKOM, lBoardAthlete, Info.NoRiders);
                ImprovementHint = RideUtil.CreateImprovementHint(Athlete.ElapsedTimes.Min, dbResult.LeaderBoards, lBoardAthlete);

                if (leaderBoardPageNo > 0)
                {
                    LeaderBoard = RideUtil.GetPagedLeaderBoards(leaderBoards, leaderBoardPageNo);
                }
            }
            else
            {
                LeaderBoard = RideUtil.CreateBlankPagedList();
                Athlete = RideUtil.CreateBlankAthleteInfo();
            }
        }

        private void SetYersey(Result dbResult)
        {
            YellowYersey = (dbResult != null) ? dbResult.YellowYerseyLB : null;
            GreenYersey = (dbResult != null) ? dbResult.GreenYerseyLB : null;
            PolkaDotYersey = (dbResult != null) ? dbResult.PolkaDotYerseyLB : null;
        }

        #endregion


    }
}
