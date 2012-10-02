using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeloTours.Models;

namespace VeloTours.Models
{
    public partial class SegmentAreaViewModel
    {

        public SegmentAreaViewModel()
        {

            this.Segments = new HashSet<Segment>();

            this.ResultPeriod = new HashSet<ResultPeriod>();

        }


        public int SegmentAreaID { get; set; }

        public int RegionID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUri { get; set; }

        public Nullable<double> Distance { get; set; }

        public Nullable<double> AvgGrade { get; set; }

        public Nullable<double> ElevationGain { get; set; }

        public Nullable<System.Guid> SecretKey { get; set; }

        public System.DateTime LastUpdated { get; set; }

        public int Position { get; set; }

        public virtual Region Region { get; set; }

        public virtual ICollection<Segment> Segments { get; set; }

        public virtual ICollection<ResultPeriod> ResultPeriod { get; set; }

    }
}