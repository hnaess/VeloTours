using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Stravan;
using Stravan.Json;
using VeloTours.Models;
using VeloTours.DAL.Segment;

namespace VeloTours.DAL.Country
{
    public class CountryUpdate
    {
        private TourModelContainer db = new TourModelContainer();
        private int countryID;
        private Models.Country dbcountry;

        #region Singletons

        private StravaWebClient _stravaWebClient;
        public StravaWebClient StravaWebClientObj
        {
            get { return _stravaWebClient ?? (_stravaWebClient = new StravaWebClient()); }
            set { _stravaWebClient = value; }
        }

        #endregion

        public CountryUpdate(int countryID)
        {
            this.countryID = countryID;
            dbcountry = db.Countries.Find(countryID);
        }

        public void Update(bool recursive)
        {
            foreach (var region in dbcountry.Regions)
            {
                Region.RegionUpdate updater = new Region.RegionUpdate(region.RegionID);
                updater.StravaWebClientObj = StravaWebClientObj;

                updater.Update(recursive);
            }
        }
    }
}