using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeloTours.Models
{
    public class AthleteRideInfo
    {
        // Others
        public Nullable<int> BehindKom { get; set; }
        public Nullable<double> BehindKomPercentage { get; set; }

        public Nullable<int> NoRidden { get; set; }
        public ElapsedTimes ElapsedTimes { get; set; }

        public Nullable<int> Position { get; set; }
        public Nullable<double> PositionPercentage { get; set; }

        public Nullable<int> UsersTimePrevious { get; set; }    // TODO: Map field
        public Nullable<int> UsersChangePos { get; set; }       // TODO:Map field
    }
}