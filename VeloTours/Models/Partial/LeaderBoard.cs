using System;
using System.Collections.Generic;
using VeloTours.Models.Extensions;

namespace VeloTours.Models
{
    public partial class LeaderBoard
    {
        public string Min { get { return ElapsedTimes.Min.ToTime(); } }
        public string Median { get { return ElapsedTimes.Median.ToTime(); } }
        public string Average { get { return ElapsedTimes.Average.ToTime(); } }
        public string Max { get { return ElapsedTimes.Max.ToTime(); } }
        public string Stdev { get { return ElapsedTimes.Stdev > 1 ? @String.Format("{0:#}", ElapsedTimes.Stdev) : ""; } }
        public string Percentile90 { get { return ElapsedTimes.Percentile90.ToTime(); } }

        public string Yellow { get { return YellowPoints.ToTime(); } }
    }
}
