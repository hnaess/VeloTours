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
    
    public partial class Result
    {
        public Result()
        {
            this.Efforts = new HashSet<Effort>();
            this.LeaderBoards = new HashSet<LeaderBoard>();
        }
    
        public int ResultID { get; set; }
        public System.DateTime LastUpdated { get; set; }
    
        public virtual Segment Segment { get; set; }
        public virtual SegmentArea SegmentArea { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<Effort> Efforts { get; set; }
        public virtual ICollection<LeaderBoard> LeaderBoards { get; set; }
        public virtual LeaderBoard YellowYersey { get; set; }
        public virtual LeaderBoard GreenYersey { get; set; }
        public virtual LeaderBoard PolkaDotYersey { get; set; }
    }
}
