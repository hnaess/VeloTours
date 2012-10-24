using System;
using System.Collections.Generic;
using System.Web;
using VeloTours.DAL.Area;

namespace VeloTours.DAL.Region
{
    public class RegionUpdate : RideUpdate
    {
        private int regionID;
        private Models.Region dbRegion;

        public RegionUpdate(int regionID)
        {
            this.regionID = regionID;
            dbRegion = db.Regions.Find(regionID);
        }

        public override void Update()
        {
            // TODO: Calculate Region
            
            foreach (var area in dbRegion.SegmentAreas)
            {
                AreaUpdate areaUpdate = new AreaUpdate(area.SegmentAreaID);
                areaUpdate.StravaWebClientObj = StravaWebClientObj;

                areaUpdate.Update();
            }
        }
    }
}