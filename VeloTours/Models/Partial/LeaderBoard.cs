using System;
using System.Collections.Generic;
using VeloTours.Models.Extensions;

namespace VeloTours.Models
{
    public partial class LeaderBoard
    {
        public string Min { get { return ElapsedTimes.Min.ToTime(); } }
        public string Median { get { return ValueOrBlankIfSameMin(ElapsedTimes.Median); } }
        public string Average { get { return ValueOrBlankIfSameMin(ElapsedTimes.Average); } }
        public string Max { get { return ValueOrBlankIfSameMin(ElapsedTimes.Max); } }
        public string Stdev { get { return ElapsedTimes.Stdev > 1 ? @String.Format("{0:#}", ElapsedTimes.Stdev) : ""; } }
        public string Percentile90 { get { return ValueOrBlankIfSameMin(ElapsedTimes.Percentile90); } }

        public string Yellow { get { return YellowPoints.ToTime(); } }

        public string GreenPointsF { get { return GreenPoints > 0 ? GreenPoints.ToString() : ""; } }
        public string PolkaDotPointsF { get { return PolkaDotPoints > 0 ? PolkaDotPoints.ToString() : ""; } }

        private string ValueOrBlankIfSameMin(int seconds)
        {
            return (seconds != ElapsedTimes.Min) ? seconds.ToTime() : "";
        }

    }
}
