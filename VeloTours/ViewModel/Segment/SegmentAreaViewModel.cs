using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using VeloTours.Models;

namespace VeloTours.ViewModels
{
    public class SegmentAreaViewModel : Ride
    {
        #region Properties

        // Navigators
        public Models.Region Region { get; set; }
        public Models.SegmentArea SegmentArea { get; set; }
        
        // Model
        public List<SegmentViewModel> Segments { get; set; }
        public override Statistics Info { get { return SegmentArea.Info; } }

        #endregion

        #region Constructors

        public SegmentAreaViewModel(int athleteID, Models.SegmentArea dbArea, int? leaderBoardPageNo)
        {
            Region = dbArea.Region;
            SegmentArea = dbArea;
            var dbResult = dbArea.Result;

            SetRide(athleteID, leaderBoardPageNo, dbResult);
            Segments = GetSementViewModels(athleteID);
        }

        #endregion

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

    }
}