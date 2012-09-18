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
            var countries = Countries(context);
            var regions = RegionsInNorway(context, countries[0]);

            var segmentInfo = OsloKlatreKonge(context, regions[0]);

            Athletes(context);
            Grades(context);
        }

        private static List<Region> RegionsInNorway(TourModelContainer context, Models.Country country)
        {
            var regions = new List<Region>
            {
                new Region { Country = country, Name = "Østlandet" },
                new Region { Country = country, Name = "Sørlandet" },
                new Region { Country = country, Name = "Midt-Norge" },
                new Region { Country = country, Name = "Nord-Norge" },
            };
            regions.ForEach(s => context.Regions.Add(s));
            context.SaveChanges();
            return regions;
        }

        private static List<Country> Countries(TourModelContainer context)
        {
            var countries = new List<Country>
            {
                new Country { Code = "NO", Name = "Norway" },
                new Country { Code = "UK", Name = "United Kingdom" },
                new Country { Code = "US", Name = "United States" },
            };
            countries.ForEach(s => context.Countries.Add(s));
            context.SaveChanges();
            
            return countries;
        }

        private static List<SegmentInfo> OsloKlatreKonge(TourModelContainer context, Models.Region region)
        {
            var segmentInfos = new List<SegmentInfo>
            {
                new SegmentInfo { 
                    ElevDifference = 372,
                    Distance = 5.6,
                    AvgGrade = 6.7,
                    Riders = 627,
                    Ridden = 2952,
                },
                new SegmentInfo { 
                    ElevDifference = 122,
                    Distance = 1.8,
                    AvgGrade = 6.7,
                    Riders = 767,
                    Ridden = 8132,
                },
                new SegmentInfo { 
                    ElevDifference = 91,
                    Distance = 2.0,
                    AvgGrade = 4.6,
                    Riders = 705,
                    Ridden = 4872,
                },

                new SegmentInfo { 
                    ElevDifference = 585,
                    Distance = 9.4,
                    AvgGrade = 6.25,
                    Riders = 0, // ?
                    Ridden = 0, // ?
                },
            };
            segmentInfos.ForEach(s => context.SegmentInfos.Add(s));
            context.SaveChanges();

            var segments = new List<Segment>
            {
                new Segment { Name = "Kongsveien", StravaID = 632847, SegmentInfo = segmentInfos[0] },
                new Segment { Name = "Klatringen til Tryvann fra Gressbanen", StravaID = 1942901, SegmentInfo = segmentInfos[1] },
                new Segment { Name = "Grefsenkollen", StravaID = 660072, SegmentInfo = segmentInfos[0] },
            };
            segments.ForEach(s => context.Segments.Add(s));
            context.SaveChanges();

            var segmentAreas = new List<SegmentArea>
            {
                new SegmentArea { 
                    Name = "Oslo klatrekonge",
                    Region = region,
                    Segment = segments,
                    LastUpdated = DateTime.Now,
                }
            };
            segmentAreas.ForEach(s => context.SegmentAreas.Add(s));
            context.SaveChanges();

            return segmentInfos;
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