using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace VeloTours.Models
{
    public class Country
    {
        public String CountryCode { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }

    public class Region
    {
        public int RegionID { get; set; }
        public virtual Country Country { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }

    public class SegmentArea
    {
        public int SegmentAreaID { get; set; }
        public virtual Region Region { get; set; }

        public virtual SegmentInfo SegmentInfo { get; set; }

        public virtual ICollection<Segment> Segments { get; set; }

        public DateTime LastUpdated { get; set; }
    }

    public class Segment
    {
        /// <summary>Same as Strava Segment Id</summary>
        public int SegmentId;

        public virtual SegmentInfo SegmentInfo { get; set; }
    }

    public class SegmentInfo
    {
        public int Length { get; set; }
        public int Riders { get; set; }
        public int ElevGain { get; set; }
        public int GradeType { get; set; }
        
        /// <summary>On SegmentArea then AvgGradeValue = Length * Distance</summary>
        public double AvgGradeValue { get; set; }

        public DateTime LastUpdated { get; set; }
    }

    public class Leaderboard
    {
        public Stats YellowYersey { get; set; }
        public Stats GreenYersey { get; set; }
        public Stats PKYersey { get; set; }
    }

    public class Stats
    {
        public virtual Athlete Athlete { get; set; }
        public TimeSpan Duration { get; set; }
        public int Points { get; set; }
    }

    public class Athlete
    {
        public int AthleteID { get; set; }
        public string Name { get; set; }
        public bool privacy { get; set; }
        
        public DateTime LastUpdated { get; set; }
    }


    //http://www.asp.net/mvc/tutorials/mvc-4/getting-started-with-aspnet-mvc4/adding-a-model
    //http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application

}