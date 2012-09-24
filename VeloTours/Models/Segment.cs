
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
    
public partial class Segment
{

    public Segment()
    {

        this.SegmentAreas = new HashSet<SegmentArea>();

    }


    public int SegmentID { get; set; }

    public string Name { get; set; }

    public int GradeType { get; set; }

    public Nullable<double> Distance { get; set; }

    public Nullable<int> ElevDifference { get; set; }

    public Nullable<double> AvgGrade { get; set; }

    public Nullable<int> NoRiders { get; set; }

    public Nullable<int> NoRidden { get; set; }

    public System.DateTime LastUpdated { get; set; }



    public virtual ICollection<SegmentArea> SegmentAreas { get; set; }

}

}
