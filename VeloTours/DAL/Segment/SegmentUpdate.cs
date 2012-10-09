using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Stravan;
using Stravan.Json;
using VeloTours.DAL.Athlete;
using VeloTours.Models;

namespace VeloTours.DAL.Segment
{
    public class SegmentUpdate
    {
        private TourModelContainer db = new TourModelContainer();
        private Models.Segment dbSegment;
        private int segmentID;
        public Dictionary<int, AthleteShortInfo> athletes;

        #region Singletons

        private StravaWebClient _stravaWebClient;
        public StravaWebClient StravaWebClientObj
        {
            get { return _stravaWebClient ?? (_stravaWebClient = new StravaWebClient()); }
            set { _stravaWebClient = value; }
        }
        
        #endregion

        #region Constructors

        public SegmentUpdate(int segmentID)
        {
            this.segmentID = segmentID;
            dbSegment = db.Segments.Find(segmentID);
        }

        public SegmentUpdate(Models.Segment dbSegment)
        {
            this.dbSegment = dbSegment;
            this.segmentID = dbSegment.SegmentID;
        }

        #endregion

        public Models.Segment UpdateSegment()
        {
            List<SegmentEffort> rides = new List<SegmentEffort>();
            Stravan.Segment segmentinfo = null;

            var originalCulture = Utils.SetStravaCultureAndReturnCurrentCulture();
            try
            {
                SegmentService serv = new SegmentService(StravaWebClientObj);
                segmentinfo = serv.Show(segmentID);                           
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = originalCulture;
            }

            Statistics info = dbSegment.Info;
            info.AvgGrade = segmentinfo.AverageGrade;
            info.ClimbCategory = segmentinfo.ClimbCategory;
            info.Distance = segmentinfo.Distance;
            info.ElevationGain = segmentinfo.ElevationGain;
            info.ElevationHigh = segmentinfo.ElevationHigh;
            info.ElevationLow = segmentinfo.ElevationLow;
            info.Name = segmentinfo.Name;
            info.LastUpdated = DateTime.UtcNow;
            db.SaveChanges();
            return dbSegment;
        }

        //public void UpdateEfforts(DateTime startDate, DateTime endDate)
        //{
        //    throw new NotImplementedException("Update based on dates");
        //}

        public void UpdateEfforts(int segmentID)
        {
            Models.Segment dbSegment = db.Segments.Find(segmentID);
            UpdateEfforts(dbSegment);
        }

        public void UpdateEfforts(Models.Segment dbSegment)
        {
            var result = AddResultSet(dbSegment);

            var efforts = new List<Models.Effort>();

            EffortUpdate effortUpdater = new EffortUpdate(db, result, dbSegment.SegmentID, (double)dbSegment.Info.ElevationGain);
            effortUpdater.StravaWebClientObj = StravaWebClientObj;

            effortUpdater.UpdateEfforts();
            
            EffortUpdateStatus rideInfo = effortUpdater.UpdateLeaderboard(dbSegment.Info.ClimbCategory);
            UpdateRideInfoOnSegment(dbSegment, rideInfo);
        }

        private void UpdateRideInfoOnSegment(Models.Segment dbSegment, EffortUpdateStatus rideInfo)
        {
            Statistics info = dbSegment.Info;
            info.NoRidden = rideInfo.ridden;
            info.NoRiders = rideInfo.riders;
            db.SaveChanges();
        }

        private Result AddResultSet(Models.Segment dbSegment)
        {
            var result = new Models.Result { Segment = dbSegment, LastUpdated = DateTime.Now };
            result = db.ResultSet.Add(result);
            db.SaveChanges();
            return result;
        }
    }
}