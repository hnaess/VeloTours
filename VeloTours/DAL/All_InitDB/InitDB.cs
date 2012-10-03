﻿using System;
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

        public static void CreateAndInitIfEmpty(TourModelContainer context)
        {
            context.Database.Delete();
            if (!context.Database.Exists())
            {
                context.Database.Create();
                Init(context);
            }
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
                new Models.Region { CountryID = 578, Name = "Oslo" },
                new Models.Region { CountryID = 578, Name = "Bærum" },
                new Models.Region { CountryID = 578, Name = "Asker" },
                new Models.Region { CountryID = 578, Name = "Nesodden" },
                new Models.Region { CountryID = 578, Name = "Trondheim" },
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

        internal static Models.SegmentArea NewSegmentArea(TourModelContainer context, Models.Region region, List<Models.Segment> segments, string name)
        {
            var result = new Models.Result { LastUpdated = DefaultDate };
            return new SegmentArea { Region = region, Name = name, Segments = segments, Result = result, LastUpdated = DefaultDate };
        }

        internal static Models.Segment NewSegment(TourModelContainer context, int segmentID, string name)
        {
            var result = new Models.Result { LastUpdated = DefaultDate };
            return new Models.Segment { SegmentID = segmentID, Name = name, Result = result, LastUpdated = DefaultDate, };
        }
    }
}