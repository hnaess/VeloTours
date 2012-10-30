using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeloTours.Models;
using VeloTours.DAL.IntilizeDB;
using VeloTours.DAL.IntilizeDB.Norway;

namespace VeloTours.DAL
{
    public class InitDB
    {
        public static DateTime DefaultDate = new DateTime(1980, 1, 1);

        public static int initDB = 0;

        public static void CreateAndInitIfEmpty(TourModelContainer context)
        {
            initDB++;
            //if (initDB <= 1)
            //    DeleteDB(context);

            if (!context.Database.Exists())
            {
                context.Database.Create();
                Init(context);
                CreateIndexes(context);
            }
        }

        private static bool DeleteDB(TourModelContainer context)
        {
            return context.Database.Delete();
        }

        private static void CreateIndexes(TourModelContainer context)
        {
            CreateIndex(context, "StartDate", "Efforts", false);
            CreateIndex(context, "ElapsedTime", "Efforts", false); // TODO: Review stil lvalid?
        }

        private static void CreateIndex(TourModelContainer context, string field, string table, bool unique = false)
        {
            context.Database.ExecuteSqlCommand(String.Format("CREATE {0}NONCLUSTERED INDEX IX_{1}_{2} ON {1} ({3})",
                unique ? "UNIQUE " : "",
                table,
                field.Replace(",", "_"),
                field));
        } 

        private static void Init(TourModelContainer context)
        {
            var athletes = AthleteData.Athletes(context);

            var countries = Countries(context);
            var regions = RegionsInNorway(context, countries[0]);

            SegmentData.OsloKlatreKonge(context, regions[0]);
            SegmentData.Baerum(context, regions[1]);
            SegmentData.Asker(context, regions[2]);
            SegmentData.Nesodden(context, regions[3]);
        }

        private static List<Models.Region> RegionsInNorway(TourModelContainer context, Models.Country country)
        {
            var regions = new List<Models.Region>
            {
                new Models.Region { CountryID = 578, Info = NewStatistics(context, "Oslo")},
                new Models.Region { CountryID = 578, Info = NewStatistics(context, "Bærum")},
                new Models.Region { CountryID = 578, Info = NewStatistics(context, "Asker")},
                new Models.Region { CountryID = 578, Info = NewStatistics(context, "Nesodden" )},
                new Models.Region { CountryID = 578, Info = NewStatistics(context, "Trondheim")},
            };
            regions.ForEach(s => context.Regions.Add(s));
            context.SaveChanges();
            return regions;
        }


        private static List<Models.Country> Countries(TourModelContainer context)
        {
            var countries = new List<Models.Country>
            {
                new Models.Country { CountryID = 578, Name = "Norway" },
                new Models.Country { CountryID = 826, Name = "United Kingdom" },
                new Models.Country { CountryID = 840, Name = "United States" },
            };
            countries.ForEach(s => context.Countries.Add(s));
            context.SaveChanges();

            return countries;
        }

        private static Statistics NewStatistics(TourModelContainer context, string name)
        {
            return new Statistics() { Name = name, AvgGrade = 0, Distance = 0, NoRidden = 0, NoRiders = 0, LastUpdated = DefaultDate };
        }

        internal static Models.SegmentArea NewSegmentArea(TourModelContainer context, Models.Region region, List<Models.Segment> segments, string name, SegmentArea.AreaTypeEnum areaType = SegmentArea.AreaTypeEnum.Default)
        {
            Models.Result result = null;
            return new SegmentArea { Region = region, Segments = segments, Result = result, Info = NewStatistics(context, name), AreaType = (int)areaType};
        }

        internal static Models.Segment NewSegment(TourModelContainer context, int segmentID, string name)
        {
            Models.Result result = null;
            return new Models.Segment { SegmentID = segmentID, Result = result, Info = NewStatistics(context, name), };
        }
    }
}