using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Stravan;
using Stravan.Json;
using VeloTours.Models;
using VeloTours.DAL.Segment;
using VeloTours.DAL.Area;

namespace VeloTours.DAL.Region
{
    public class Update
    {
        private TourModelContainer db = new TourModelContainer();
        private int regionID;
        private Models.Region dbRegion;

        #region Singletons

        private StravaWebClient _stravaWebClient;
        public StravaWebClient StravaWebClientObj
        {
            get { return _stravaWebClient ?? (_stravaWebClient = new StravaWebClient()); }
            set { _stravaWebClient = value; }
        }

        #endregion

        public Update(int regionID)
        {
            this.regionID = regionID;
            dbRegion = db.Regions.Find(regionID);
        }

        public void UpdateRegion(bool recursive)
        {
            foreach (var area in dbRegion.SegmentAreas)
            {
                AreaUpdate updater = new AreaUpdate(area.SegmentAreaID);
                updater.StravaWebClientObj = StravaWebClientObj;

                updater.UpdateArea(recursive);
            }
        }
    }
}