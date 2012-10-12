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
    
    public partial class Statistics
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUri { get; set; }
        public double Distance { get; set; }
        public double AvgGrade { get; set; }
        public string ClimbCategory { get; set; }
        public Nullable<double> ElevationHigh { get; set; }
        public Nullable<double> ElevationLow { get; set; }
        public Nullable<double> ElevationGain { get; set; }
        public int NoRiders { get; set; }
        public int NoRidden { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
    }
}
