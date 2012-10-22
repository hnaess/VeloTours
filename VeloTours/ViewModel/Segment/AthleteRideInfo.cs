using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeloTours.Models;
using VeloTours.Models.Extensions;

namespace VeloTours.ViewModels
{
    public partial class AthleteRideInfo
    {
        public AthleteRideInfo()
        {
            ElapsedTimes = new ElapsedTimes();
        }
        
        // Others
        public Nullable<int> BehindKom { get; set; }
        public Nullable<double> BehindKomPercentage { get; set; }
        public string BehindKomPercentageF { get { return String.Format("{0:#0.#}", BehindKomPercentage); } }

        public Nullable<int> NoRidden { get; set; }
        public ElapsedTimes ElapsedTimes { get; set; }

        public Nullable<int> Position { get; set; }
        public Nullable<double> PositionPercentage { get; set; }
        public string PositionPercentageF { get { return String.Format("{0:#0.#}", BehindKomPercentage); } }

        public Nullable<int> UsersTimePrevious { get; set; }    // TODO: Map field
        public Nullable<int> UsersChangePos { get; set; }       // TODO:Map field
    }

    public partial class AthleteRideInfo
    {
        public string Min { get { return ElapsedTimes.Min.ToTime(); } }
        public string Median { get { return ElapsedTimes.Median.ToTime(); } }
        public string Average { get { return ElapsedTimes.Average.ToTime(); } }
        public string Max { get { return ElapsedTimes.Max.ToTime(); } }
        public string Stdev { get { return @String.Format("{0:#}", ElapsedTimes.Stdev); } }
        public string Percentile90 { get { return ElapsedTimes.Percentile90.ToTime(); } }
    }
}