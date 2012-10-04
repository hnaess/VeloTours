using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeloTours.Models;

namespace VeloTours.DAL.IntilizeDB
{
    internal class AthleteData
    {
        internal static List<Models.Athlete> Athletes(TourModelContainer context)
        {
            var athletes = new List<Models.Athlete>
            {
                new Models.Athlete { AthleteID = 352657, Name = "Henrik Næss", PrivacyMode = 1, LastUpdated = DateTime.Now },
                new Models.Athlete { AthleteID = 354993, Name = "Fredrik Massey", PrivacyMode = 1, LastUpdated = DateTime.Now },
            };
            athletes.ForEach(s => context.Athletes.Add(s));
            context.SaveChanges();

            return athletes;
        }
    }
}