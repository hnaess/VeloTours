﻿using System;
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
    public class AreaUpdate
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

        public AreaUpdate(int areaID)
        {
            this.areaID = areaID;
            dbArea = db.SegmentAreas.Find(areaID);
        }

        public void UpdateArea(bool updateEfforts)
        {
            if (dbArea.Segments.Count == 0)
                return; // TODO: Logging?

            Statistics info = dbArea.Info;
            info.Distance = 0;
            info.ElevationGain = 0;
            decimal avgGradeTemp = 0;

            foreach (var segment in dbArea.Segments)
            {
                SegmentUpdate segmentUpdater = new SegmentUpdate(segment.SegmentID);
                segmentUpdater.StravaWebClientObj = StravaWebClientObj;

                var segmentInfo = segmentUpdater.UpdateSegment();
                if (updateEfforts)
                {
                    segmentUpdater.UpdateEfforts(segmentInfo);
                }

                info.Distance += segmentInfo.Info.Distance;
                info.ElevationGain += segmentInfo.Info.ElevationGain;
                avgGradeTemp += Convert.ToDecimal(segmentInfo.Info.Distance) * Convert.ToDecimal(segmentInfo.Info.AvgGrade);
            }

            info.AvgGrade = Convert.ToDouble((avgGradeTemp / Convert.ToDecimal(info.Distance)));
            db.SaveChanges();
        }
    }
}