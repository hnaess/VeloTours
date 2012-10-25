using System;
using System.Collections.Generic;
using System.Web;
using VeloTours.Models;
using VeloTours.DAL.Segment;

namespace VeloTours.DAL.Country
{
    public class CountryUpdate : RideUpdate
    {
        private int countryID;
        private Models.Country dbcountry;

        public CountryUpdate(int countryID)
        {
            this.countryID = countryID;
            dbcountry = db.Countries.Find(countryID);
        }

        public override bool Update()
        {
            foreach (var region in dbcountry.Regions)
            {
                Region.RegionUpdate countryUpdate = new Region.RegionUpdate(region.RegionID);
                countryUpdate.StravaWebClientObj = StravaWebClientObj;

                countryUpdate.Update();
            }
            return true;
        }
    }
}