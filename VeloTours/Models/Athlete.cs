
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
    
public partial class Athlete
{

    public Athlete()
    {

        this.LeaderBoards = new HashSet<LeaderBoard>();

        this.Efforts = new HashSet<Effort>();

    }


    public int AthleteID { get; set; }

    public string Name { get; set; }

    public Nullable<int> PrivacyMode { get; set; }

    public System.DateTime LastUpdated { get; set; }



    public virtual ICollection<LeaderBoard> LeaderBoards { get; set; }

    public virtual ICollection<Effort> Efforts { get; set; }

}

}
