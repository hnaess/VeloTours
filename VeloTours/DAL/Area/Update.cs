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

        public void UpdateArea(bool updateEfforts)
        {
            dbArea.Distance = 0;
            dbArea.ElevationGain = 0;
            decimal avgGradeTemp = 0;

            foreach (var segment in dbArea.Segments)
            {
                SegmentUpdate segmentUpdater = new SegmentUpdate(segment.SegmentID);
                segmentUpdater.StravaWebClientObj = StravaWebClientObj;

                var info = segmentUpdater.UpdateSegment(updateEfforts);
                dbArea.Distance += info.Distance;
                dbArea.ElevationGain += info.ElevationGain;
                avgGradeTemp += Convert.ToDecimal(info.Distance) * Convert.ToDecimal(info.AvgGrade);
            }

            dbArea.AvgGrade = Convert.ToDouble((avgGradeTemp / Convert.ToDecimal(dbArea.Distance)));
            db.SaveChanges();
        }
    }
}