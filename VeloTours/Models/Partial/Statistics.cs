using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeloTours.Models
{
    public partial class Statistics
    {
        public string AvgGradeString { get { return String.Format("{0:#0.#}", AvgGrade); } }
        public string DistanceInKm { get { return String.Format("{0:#0.##}", Distance / 1000); } }

        public double SpeedInKmH(LeaderBoard lboard)
        {
            return (Distance / lboard.ElapsedTimes.Min) * 3.6;
        }

    }
}