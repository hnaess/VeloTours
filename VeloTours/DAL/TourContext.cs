using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using VeloTours.Models;
using System.Data.Entity;

namespace VeloTours.Models
{
    public class TourContext : DbContext
    {
        //public DbSet<Athlete> Athletes { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<SegmentArea> SegmentArea { get; set; }

        public DbSet<Segment> Segments { get; set; }
        //public DbSet<SegmentInfo> SegmentInfos { get; set; }
        //public DbSet<LeaderBoard> LeaderBoards { get; set; }
        //public DbSet<Statistic> Statistic { get; set; }

        public DbSet<Athlete> Athletes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}