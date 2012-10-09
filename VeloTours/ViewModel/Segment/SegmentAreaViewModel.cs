using System;
using System.Collections.Generic;

namespace VeloTours.Models
{
    public class SegmentAreaViewModel
    {
        public Models.SegmentArea SegmentArea { get; set; }
        public Nullable<double> KomSpeed { get; set; }
        public AthleteRideInfo Athlete { get; set; }

        public List<SegmentViewModel> Segments { get; set; }
   }
}