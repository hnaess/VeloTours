using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using VeloTours.Models;

namespace VeloTours.ViewModels
{
    public class RegionViewModel : Ride
    {
        #region Properties 

        // Navigators
        public Models.Country Country { get; set; }
        public Models.Region Region { get; set; }

        // Model
        public List<SegmentAreaViewModel> SegmentAreas { get; set; }
        public override Statistics Info { get { return Region.Info; } }

        #endregion

        #region Constructors

        public RegionViewModel(int athleteID, Models.Region dbRegion, int? leaderBoardPageNo)
        {
            Country = dbRegion.Country;
            Region = dbRegion;
            var dbResult = dbRegion.Result;

            if (SetRide(athleteID, leaderBoardPageNo, dbResult))
            {
                SegmentAreas = GetSegmentAreasViewModels(athleteID);
            }
        }

        #endregion

        private List<SegmentAreaViewModel> GetSegmentAreasViewModels(int athleteID)
        {
            throw new NotImplementedException("TODO");
        }
    }
}