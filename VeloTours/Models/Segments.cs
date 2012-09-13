using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace VeloTours.Models
{
    public class Leaderboard
    {
        public Stats YellowYersey { get; set; }
        public Stats GreenYersey { get; set; }
        public Stats PKYersey { get; set; }
        // Youth?
    }

    public class Segments
    {
        public int ID { get; set; }
        public int CountryID { get; set; }

        public List<Segments> SegmentList { get; set; }

        public int Length { get; set; }
        public int Riders { get; set; }
        public int ElevGain { get; set; }
        public int GradeType { get; set; }
        /// <summary>AvgGradeValue = Length * Distance</summary>
        public double AvgGradeValue { get; set; }

        public DateTime LastUpdated { get; set; }
    }

    public class Stats
    {
        public int AthleteId { get; set; }
        public TimeSpan Duration { get; set; }
        public int Points { get; set; }
    }

    public class SegmentsDBContext : DbContext
    {
        public DbSet<Segments> Movies { get; set; }
    }
}