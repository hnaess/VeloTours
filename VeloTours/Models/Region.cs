//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VeloTours.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Region
    {
        public Region()
        {
            this.SegmentAreas = new HashSet<SegmentArea>();
            this.Info = new Statistics();
        }
    
        public int RegionID { get; set; }
        public int CountryID { get; set; }
    
        public Statistics Info { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual ICollection<SegmentArea> SegmentAreas { get; set; }
        public virtual Result Result { get; set; }
    }
}
