using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using VeloTours.Models;

namespace VeloTours.ViewModels
{
    public class RegionViewModel : RideViewModel
    {
        #region Properties

        // Navigators
        public Models.Country Country { get; set; }
        // Model
        public Models.Region Region { get; set; }
        public override Statistics Info { get { return Region.Info; } }
        public override int RideID { get { return Region.RegionID; } }
        // ViewModel
        public List<SegmentAreaViewModel> SegmentAreas { get; set; }
        public override IEnumerable<RideViewModel> RideList { get { return SegmentAreas; } }
        public override bool IsMTB { get { return false; } }

        #endregion

        #region Constructors

        public RegionViewModel(int athleteID, Models.Region dbRegion, int? leaderBoardPageNo)
        {
            Country = dbRegion.Country;
            Region = dbRegion;
            var dbResult = dbRegion.Result;
            rideType = RideType.Region;

            SetRide(athleteID, leaderBoardPageNo, dbResult);
            SegmentAreas = GetSegmentAreasViewModels(athleteID);
        }

        #endregion

        private List<SegmentAreaViewModel> GetSegmentAreasViewModels(int athleteID)
        {
            var areas = new List<SegmentAreaViewModel>();
            foreach (var area in Region.SegmentAreas.OrderBy(x => x.Info.Name))
            {
                var areaViewModel = new SegmentAreaViewModel(athleteID, area, null);
                areas.Add(areaViewModel);
            }

            return areas;
        }
    }
}