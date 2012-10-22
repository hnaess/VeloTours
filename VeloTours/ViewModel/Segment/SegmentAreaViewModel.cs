using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using VeloTours.Models;

namespace VeloTours.ViewModels
{
    public class SegmentAreaViewModel
    {
        #region Properties

        public Models.Region Region { get; set; }
        public Models.SegmentArea SegmentArea { get; set; }
        public Nullable<double> KomSpeed { get; set; }
        public AthleteRideInfo Athlete { get; set; }

        public List<SegmentViewModel> Segments { get; set; }
        
        public Statistics Info { get { return SegmentArea.Info; } }
        public IPagedList<Models.LeaderBoard> LeaderBoard { get; set; } 

        public LeaderBoard YellowYersey { get; set; }
        public LeaderBoard GreenYersey { get; set; }
        public LeaderBoard PolkaDotYersey { get; set; }

        #endregion

        #region Constructors

        public SegmentAreaViewModel(int athleteID, Models.SegmentArea dbArea, int? leaderBoardPageNo)
        {
            var dbResult = dbArea.Result;

            SegmentArea = dbArea;
            Region = dbArea.Region;

            SetYersey(dbResult);

            if (RideUtil.LeaderBoardExists(dbResult)) 
            {
                var leaderBoards = dbResult.LeaderBoards;
                var lBoardKOM = leaderBoards.OrderBy(x => x.ElapsedTimes.Min).First();
                var lBoardAthlete = dbResult.LeaderBoards.SingleOrDefault(x => x.AthleteID == athleteID);

                KomSpeed = Info.SpeedInKmH(lBoardKOM);
                LeaderBoard = RideUtil.GetPagedLeaderBoards(leaderBoards, leaderBoardPageNo);
                Athlete = RideUtil.AddAthleteToViewModel(athleteID, leaderBoards, lBoardKOM, lBoardAthlete, Info.NoRiders);
                Segments = GetSementViewModels(athleteID);
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

        private List<SegmentViewModel> GetSementViewModels(int athleteID)
        {
            List<SegmentViewModel> segments = new List<SegmentViewModel>();
            foreach (var segment in SegmentArea.Segments)
            {
                var segmentViewModel = new SegmentViewModel(athleteID, segment, null);
                segments.Add(segmentViewModel);
            }

            return segments;
        }

        #endregion
    }
}