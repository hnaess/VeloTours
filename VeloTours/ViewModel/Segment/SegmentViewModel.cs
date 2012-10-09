using System;
using System.Collections.Generic;

namespace VeloTours.Models
{
    public class SegmentViewModel
    {
        public Models.Segment Segment { get; set; }
        public Nullable<double> KomSpeed { get; set; }
        public AthleteRideInfo Athlete { get; set; }
    }
}
