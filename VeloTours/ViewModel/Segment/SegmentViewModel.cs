﻿using System;
using System.Collections.Generic;

namespace VeloTours.Models
{
    public class SegmentViewModel
    {
        public Models.Segment Segment { get; set; }
        public Nullable<double> KomSpeed { get; set; }
        public AthleteRideInfo Athlete { get; set; }

        public Statistics Info { get { return Segment.Info; } }
        public ICollection<Models.LeaderBoard> LeaderBoard { get { return Segment.Result.LeaderBoards; } }
    }
}
