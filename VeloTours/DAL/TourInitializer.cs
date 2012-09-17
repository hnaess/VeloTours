﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using VeloTours.Models;

namespace VeloTours.DAL
{
    public class TourInitializer : DropCreateDatabaseIfModelChanges<TourContext>
    {
        protected override void Seed(TourContext context)
        {
            var athletes = new List<Athlete>
            {
                new Athlete { AthleteID = 352657, Name = "Henrik Næss", LastUpdated = DateTime.Now },
                new Athlete { AthleteID = 354993, Name = "Fredrik Massey", LastUpdated = DateTime.Now },
            };
            athletes.ForEach(s => context.Athletes.Add(s));
            context.SaveChanges();

            var countries = new List<Country>
            {
                new Country { CountryID = "NO", Name = "Norway", Active = true, },
                new Country { CountryID = "UK", Name = "United Kingdom", Active = false, },
                new Country { CountryID = "US", Name = "United States", Active = false, },
            };
            countries.ForEach(s => context.Countries.Add(s));
            context.SaveChanges();

            var regions = new List<Region>
            {
                new Region { Country = countries[0], Name = "Østlandet", Active = true },
                new Region { Country = countries[0], Name = "Midt-Norge", Active = true },
            };
            regions.ForEach(s => context.Regions.Add(s));
            context.SaveChanges();

            var segmentsArea = new List<SegmentArea>
            {
                new SegmentArea { Region = regions[0], SegmentAreaID = 1, Name = "Oslo klatrekong", LastUpdated = DateTime.Now, Segments = 
                    new List<Segment>
                    { 
                        new Segment { SegmentID = 1942901 },
                        new Segment { SegmentID = 660072 },
                        new Segment { SegmentID = 632847 },
                    }
                }
            };
            segmentsArea.ForEach(s => context.SegmentArea.Add(s));
            context.SaveChanges();
        }
    }
}