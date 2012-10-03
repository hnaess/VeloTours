using System;
using System.Collections.Generic;

namespace VeloTours.Models
{
    public class SegmentAreaViewModel
    {
        public SegmentAreaViewModel()
        {
            this.Segments = new HashSet<SegmentViewModel>();
            //this.ResultPeriod = new HashSet<ResultPeriod>();
        }

        public Nullable<double> AvgGrade { get; set; }
        public string Description { get; set; }
        public Nullable<double> Distance { get; set; }
        public Nullable<double> ElevationGain { get; set; }
        public System.DateTime LastUpdated { get; set; }
        public string Name { get; set; }
        public Nullable<int> NoRiders { get; set; }
        public string PictureUri { get; set; }
        public int Position { get; set; }
        public virtual Region Region { get; set; }
        public int RegionID { get; set; }
        public Nullable<System.Guid> SecretKey { get; set; }
        public int SegmentAreaID { get; set; }
        public virtual ICollection<SegmentViewModel> Segments { get; set; }

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