﻿using System;
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
        // Model
        public Models.Region Region { get; set; }
        public override Statistics Info { get { return Region.Info; } }
        // ViewModel
        public List<SegmentAreaViewModel> SegmentAreas { get; set; }

        #endregion

        #region Constructors

        public RegionViewModel(int athleteID, Models.Region dbRegion, int? leaderBoardPageNo)
        {
            Country = dbRegion.Country;
            Region = dbRegion;
            var dbResult = dbRegion.Result;

            SetRide(athleteID, leaderBoardPageNo, dbResult);
            SegmentAreas = GetSegmentAreasViewModels(athleteID);
        }

        #endregion

        private List<SegmentAreaViewModel> GetSegmentAreasViewModels(int athleteID)
        {
            var areas = new List<SegmentAreaViewModel>();
            foreach (var area in Region.SegmentAreas)
            {
                var areaViewModel = new SegmentAreaViewModel(athleteID, area, null);
                areas.Add(areaViewModel);
            }

            return areas;
        }
    }
}