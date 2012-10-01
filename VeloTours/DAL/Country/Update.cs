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
    public class Update
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

        public Update(int countryID)
        {
            this.countryID = countryID;
            dbcountry = db.Countries.Find(countryID);
        }

        public void UpdateCountry(bool recursive)
        {
            foreach (var region in dbcountry.Regions)
            {
                Region.Update updater = new Region.Update(region.RegionID);
                updater.StravaWebClientObj = StravaWebClientObj;

                updater.UpdateRegion(recursive);
            }
        }
    }
}