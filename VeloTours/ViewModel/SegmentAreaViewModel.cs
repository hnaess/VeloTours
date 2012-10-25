using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using VeloTours.Models;

namespace VeloTours.ViewModels
{
    public class SegmentAreaViewModel : RideViewModel
    {
        #region Properties

        // Navigators
        public Models.Region Region { get; set; }
        // Model
        public Models.SegmentArea SegmentArea { get; set; }
        public override Statistics Info { get { return SegmentArea.Info; } }
        public override int RideID { get { return SegmentArea.SegmentAreaID; } }
        // View Model
        public List<SegmentViewModel> Segments { get; set; }
        public override IEnumerable<RideViewModel> RideList { get { return Segments; } }

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
            var segments = new List<SegmentViewModel>();
            foreach (var segment in SegmentArea.Segments.OrderBy( x => x.Info.Name))
            {
                var segmentViewModel = new SegmentViewModel(athleteID, segment, null);
                segments.Add(segmentViewModel);
            }

            return segments;
        }
    }
}