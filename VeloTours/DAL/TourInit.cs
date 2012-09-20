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
            var athletes = Athletes(context);

            var countries = Countries(context);
            var regions = RegionsInNorway(context, countries[0]);

            OsloKlatreKonge(context, regions[0]);
        }

        private static void OsloKlatreKonge(TourModelContainer context, Models.Region region)
        {
            var segments = new List<Segment>
            {
                new Segment { 
                    Name = "Kongsveien", 
                    StravaID = 632847, 
                    GradeType = 1,
                    Distance = 5.6,
                    ElevDifference = 372,
                    AvgGrade = 6.7,
                    NoRiders = 627,
                    NoRidden = 2952,
                    LastUpdated = new DateTime(2012, 09, 18, 15, 0, 0),
                },
                new Segment { 
                    Name = "Klatringen til Tryvann fra Gressbanen", 
                    StravaID = 1942901,
                    Distance = 1.8,
                    ElevDifference = 122,
                    AvgGrade = 6.7,
                    NoRiders = 767,
                    NoRidden = 8132,
                    LastUpdated = new DateTime(2012, 09, 18, 15, 0, 0),
                },
                new Segment { 
                    Name = "Grefsenkollen", 
                    StravaID = 660072,
                    ElevDifference = 91,
                    Distance = 2.0,
                    AvgGrade = 4.6,
                    NoRiders = 705,
                    NoRidden = 4872,
                    LastUpdated = new DateTime(2012, 09, 18, 15, 0, 0),
                }
            };
            segments.ForEach(s => context.Segments.Add(s));
            context.SaveChanges();

            var segmentAreas = new List<SegmentArea>
            {
                new SegmentArea { 
                    Name = "Oslo klatrekonge",
                    Region = region,
                    Segment = segments,
                    Distance = 9.4,
                    ElevDifference = 585,
                    AvgGrade = 6.25,
                    LastUpdated = new DateTime(2012, 09, 18, 15, 0, 0),
                }
            };
            segmentAreas.ForEach(s => context.SegmentAreas.Add(s));
            context.SaveChanges();

            UpdateResultForOsloKlatekonge();
        }

        private static void UpdateResultForOsloKlatekonge()
        {
            //throw new NotImplementedException();
        }

        private static List<Athlete> Athletes(TourModelContainer context)
        {
            var athletes = new List<Athlete>
            {
                new Athlete { AthleteID = 352657, Name = "Henrik Næss", PrivacyMode = 1, LastUpdated = DateTime.Now },
                new Athlete { AthleteID = 354993, Name = "Fredrik Massey", PrivacyMode = 1, LastUpdated = DateTime.Now},
            };
            athletes.ForEach(s => context.Athletes.Add(s));
            context.SaveChanges();

            return athletes;
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

    }
}