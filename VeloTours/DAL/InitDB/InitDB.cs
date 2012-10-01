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
        public static void CreateAndInitIfEmpty(TourModelContainer context)
        {
            //context.Database.Delete();
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

        private static List<Region> RegionsInNorway(TourModelContainer context, Models.Country country)
        {
            var regions = new List<Region>
            {
                new Region { CountryID = 578, Name = "Oslo" },
                new Region { CountryID = 578, Name = "Bærum" },
                new Region { CountryID = 578, Name = "Asker" },
                new Region { CountryID = 578, Name = "Nesodden" },
                new Region { CountryID = 578, Name = "Trondheim" },
            };
            regions.ForEach(s => context.Regions.Add(s));
            context.SaveChanges();
            return regions;
        }

        private static List<Country> Countries(TourModelContainer context)
        {
            var countries = new List<Country>
            {
                new Country { CountryID = 578, Name = "Norway" },
                new Country { CountryID = 826, Name = "United Kingdom" },
                new Country { CountryID = 840, Name = "United States" },
            };
            countries.ForEach(s => context.Countries.Add(s));
            context.SaveChanges();

            return countries;
        }
    }
}