using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Stravan;
using Stravan.Json;
using VeloTours.Models;
using VeloTours.DAL.Segment;

namespace VeloTours.DAL.Area
{
    public class Update
    {
        private TourModelContainer db = new TourModelContainer();
        private int areaID; 
        private Models.SegmentArea dbArea;

        #region Singletons

        private StravaWebClient _stravaWebClient;
        public StravaWebClient StravaWebClientObj
        {
            get { return _stravaWebClient ?? (_stravaWebClient = new StravaWebClient()); }
            set { _stravaWebClient = value; }
        }
        
        #endregion

        public Update(int areaID)
        {
            this.areaID = areaID;
            dbArea = db.SegmentAreas.Find(areaID);
        }

        public void UpdateArea(bool recursive)
        {
            List<Models.Segment> segmentsInfo = new List<Models.Segment>();
            foreach (var segment in dbArea.Segments)
            {
                Segment.Update updater = new Segment.Update(segment.SegmentID);
                updater.StravaWebClientObj = StravaWebClientObj;

                if (recursive)
                {
                    updater.UpdateRides();
                }
                
                var info = updater.UpdateSegmentInfo();
                segmentsInfo.Add(info);
            }
        }
    }
}