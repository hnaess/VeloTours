using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using VeloTours.Models;

namespace VeloTours.DAL
{
    public class TourInitializer : DropCreateDatabaseIfModelChanges<TourModelContainer>
    {
        protected override void Seed(TourModelContainer context)
        {
            var countries = new List<Country>
            {
                new Country { Code = "NO", Name = "Norway" },
                new Country { Code = "UK", Name = "United Kingdom" },
                new Country { Code = "US", Name = "United States" },
            };
            countries.ForEach(s => context.Countries.Add(s));
            context.SaveChanges();

            var regions = new List<Region>
            {
                new Region { Country = countries[0], Name = "Østlandet" },
                new Region { Country = countries[0], Name = "Sørlandet" },
                new Region { Country = countries[0], Name = "Midt-Norge" },
                new Region { Country = countries[0], Name = "Nord-Norge" },
            };
            regions.ForEach(s => context.Regions.Add(s));
            context.SaveChanges();

            var segmentInfos = new List<SegmentInfo>
            {
                new SegmentInfo { 
                    ElevDifference = 372,
                    Distance = 5.6,
                    AvgGrade = 6.7,
                    Riders = 767,
                }
            };

            //var segmentsArea = new List<SegmentArea>
            //{
            //    new SegmentArea { Region = regions[0], Name = "Oslo klatrekonge", LastUpdated = DateTime.Now, 
            //        new List<Segment>
            //        { 
            //            new Segment { SegmentID = 1942901 },
            //            new Segment { SegmentID = 660072 },
            //            new Segment { SegmentID = 632847 },
            //        }
            //    }
            //};
            //segmentsArea.ForEach(s => context.SegmentArea.Add(s));
            //context.SaveChanges();

            Athletes(context);
            Grades(context);
        }

        private static void Grades(TourModelContainer context)
        {
            var grades = new List<Grade>
            {
                new Grade { Climb = 50, Sprint = 50, },
                new Grade { Climb = 100, Sprint = 0, },
                new Grade { Climb = 0, Sprint = 100, },
            };
            grades.ForEach(s => context.Grades.Add(s));
            context.SaveChanges();
        }

        private static void Athletes(TourModelContainer context)
        {
            var athletes = new List<Athlete>
            {
                new Athlete { AthleteID = 352657, Name = "Henrik Næss", LastUpdated = DateTime.Now },
                new Athlete { AthleteID = 354993, Name = "Fredrik Massey", LastUpdated = DateTime.Now },
            };
            athletes.ForEach(s => context.Athletes.Add(s));
            context.SaveChanges();
        }
    }
}