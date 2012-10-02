using System;
using System.Collections.Generic;

namespace VeloTours.Models
{
    public class SegmentViewModel
    {
        public SegmentViewModel()
        {
            this.SegmentAreas = new HashSet<SegmentArea>();
        }

        // From the Model
        public Nullable<double> AvgGrade { get; set; }
        public string ClimbCategory { get; set; }
        public string Description { get; set; }
        public Nullable<double> Distance { get; set; }
        public Nullable<double> ElevationGain { get; set; }
        public Nullable<double> ElevationHigh { get; set; }
        public Nullable<double> ElevationLow { get; set; }
        public int GradeType { get; set; }
        public System.DateTime LastUpdated { get; set; }
        public string Name { get; set; }
        public Nullable<int> NoRidden { get; set; }
        public Nullable<int> NoRiders { get; set; }
        public string PictureUri { get; set; }
        public virtual ICollection<SegmentArea> SegmentAreas { get; set; }
        public int SegmentID { get; set; }

        // Others
        public Nullable<double> KomSpeed { get; set; }
        public Nullable<int> BehindKom { get; set; }
        public Nullable<double> BehindKomPercentage { get; set; }
        public Nullable<int> UsersPosition { get; set; }
        public Nullable<double> UsersPositionPercentage { get; set; }
        public Nullable<int> UsersTime { get; set; }
        public Nullable<int> UsersTimePrevious { get; set; }
        public Nullable<int> UsersChangePos { get; set; }
    }
}
