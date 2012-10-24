using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using VeloTours.Models;

namespace VeloTours.ViewModels
{
    public abstract class RideViewModel
    {
        #region Module

        public Nullable<double> KomSpeed { get; set; }
        public AthleteRideInfo Athlete { get; set; }

        public IPagedList<Models.LeaderBoard> LeaderBoard { get; set; }

        public LeaderBoard YellowYersey { get; set; }
        public LeaderBoard GreenYersey { get; set; }
        public LeaderBoard PolkaDotYersey { get; set; }

        public virtual Statistics Info { get; private set; }
        public virtual int RideID { get; private set; }

        #endregion

        #region

        protected Models.LeaderBoard lBoardAthlete = null;
        public virtual IEnumerable<RideViewModel> RideList { get; protected set; }

        #endregion

        public String KomSpeedString { get { return (KomSpeed == null) ? null : String.Format("{0:#0.0}", KomSpeed); } }
        public double CalculateSpeed(Models.LeaderBoard leaderBoard) { return (this.Info.Distance / leaderBoard.ElapsedTimes.Min) * 3.6; }

        protected bool SetRide(int athleteID, int? leaderBoardPageNo, Result dbResult)
        {
            SetYersey(dbResult);

            bool hasLeaderBoard = RideUtil.LeaderBoardExists(dbResult);
            if (hasLeaderBoard)
            {
                var leaderBoards = dbResult.LeaderBoards;
                var lBoardKOM = leaderBoards.OrderBy(x => x.ElapsedTimes.Min).First();
                lBoardAthlete = leaderBoards.SingleOrDefault(x => x.AthleteID == athleteID);

                Athlete = RideUtil.AddAthleteToViewModel(athleteID, leaderBoards, lBoardKOM, lBoardAthlete, Info.NoRiders);
                KomSpeed = Info.SpeedInKmH(lBoardKOM);

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
            return hasLeaderBoard;
        }

        protected void SetYersey(Result dbResult)
        {
            YellowYersey = (dbResult != null) ? dbResult.YellowYerseyLB : null;
            GreenYersey = (dbResult != null) ? dbResult.GreenYerseyLB : null;
            PolkaDotYersey = (dbResult != null) ? dbResult.PolkaDotYerseyLB : null;
        }
    }
}
