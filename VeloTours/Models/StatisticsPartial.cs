using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeloTours.Models
{
    public partial class Statistics
    {
        public string DistanceInKm { get { return String.Format("{0:#0.##}", Distance / 1000); } }
        public string AvgGradeString { get { return String.Format("{0:#0.#}", AvgGrade); } }
    }
}