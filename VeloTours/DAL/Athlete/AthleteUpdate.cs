using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using VeloTours.Models;

namespace VeloTours.DAL.Athlete
{
    /// <summary>
    /// Purpose is to save any new athletes, if any. Rather than segment for each new segment effort, if the user exist or not.
    /// </summary>
    public class AthleteUpdate
    {
        static readonly ObjectCache Cache = MemoryCache.Default;
        private const string KeyAthlete = "athletes";
        private const int HoursInCache = 4;

        public static Dictionary<int, AthleteShortInfo> Athletes 
        {
            get
            {
                Dictionary<int, AthleteShortInfo> athleteCache;
                if (Cache[KeyAthlete] == null)
                {
                    athleteCache = InitilizeCacheFromDb();
                    Cache.Set(KeyAthlete, athleteCache, new CacheItemPolicy() { AbsoluteExpiration = DateTimeOffset.Now.AddHours(HoursInCache) });
                }
                else
                {
                    athleteCache = (Dictionary<int, AthleteShortInfo>)Cache[KeyAthlete];
                }
                return athleteCache;
            }
            set
            {
                Cache[KeyAthlete] = value;
            }
        }

        private static Dictionary<int, AthleteShortInfo> InitilizeCacheFromDb()
        {
            Dictionary<int, AthleteShortInfo> athleteCache;
            athleteCache = new Dictionary<int, AthleteShortInfo>();
            TourModelContainer db = new TourModelContainer();
            foreach (var dbAthlete in db.Athletes.AsEnumerable())
            {
                athleteCache[dbAthlete.AthleteID] = new AthleteShortInfo() { Name = dbAthlete.Name, State = AthleteShortInfo.DbState.Saved };
            }
            return athleteCache;
        }

        public static void UpdateAthlete()
        {
            TourModelContainer db = new TourModelContainer();
            foreach(var athlete in Athletes)
            {
                int athleteID = athlete.Key;
                Models.Athlete dbAthlete = null;
                switch(athlete.Value.State)
                {
                    case AthleteShortInfo.DbState.Update:
                        dbAthlete = db.Athletes.Find(athleteID);
                        break;
                    case AthleteShortInfo.DbState.New:
                    case AthleteShortInfo.DbState.Unknown:
                        dbAthlete = new Models.Athlete() { AthleteID = athleteID, PrivacyMode = 0 };
                        db.Athletes.Add(dbAthlete);
                        break;
                    case AthleteShortInfo.DbState.Saved:
                    default:
                        break;
                }
                if(dbAthlete != null)
                {
                    dbAthlete.Name = athlete.Value.Name;
                    dbAthlete.LastUpdated = DateTime.UtcNow;
                    athlete.Value.State = AthleteShortInfo.DbState.Saved;
                }
            }
            db.SaveChanges();
        }
    }
}