using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Stravan;
using Stravan.Json;
using VeloTours.Models;

namespace VeloTours.DAL.Segment
{
    public class Update
    {
        private TourModelContainer db = new TourModelContainer();
        private int segmentID;
        private Models.Segment dbSegment;

        #region Singletons

        private StravaWebClient _stravaWebClient;
        public StravaWebClient StravaWebClientObj
        {
            get { return _stravaWebClient ?? (_stravaWebClient = new StravaWebClient()); }
            set { _stravaWebClient = value; }
        }
        
        #endregion

        public Update(int segmentID)
        {
            this.segmentID = segmentID;
            dbSegment = db.Segments.Find(segmentID);
        }

        public Models.Segment UpdateSegmentInfo()
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

            dbSegment.AvgGrade = segmentinfo.AverageGrade;
            dbSegment.ClimbCategory = segmentinfo.ClimbCategory;
            dbSegment.Distance = segmentinfo.Distance;
            dbSegment.ElevationGain = segmentinfo.ElevationGain;
            dbSegment.ElevationHigh = segmentinfo.ElevationHigh;
            dbSegment.ElevationLow = segmentinfo.ElevationLow;
            dbSegment.Name = segmentinfo.Name;
            dbSegment.LastUpdated = DateTime.UtcNow;
            db.SaveChanges();
            
            return dbSegment; 
        }

        public List<SegmentEffort> UpdateRides()
        {
            List<SegmentEffort> efforts = GetEffortsFromStrava();

            return null;
        }

        private List<SegmentEffort> GetEffortsFromStrava()
        {
            List<SegmentEffort> rides = new List<SegmentEffort>();

            var originalCulture = Utils.SetStravaCultureAndReturnCurrentCulture();
            try
            {
                SegmentService serv = new SegmentService(StravaWebClientObj);

                Stravan.Segment segmentinfo = serv.Show(segmentID);

                SegmentEfforts segmentEfforts = null;
                int offset = 0;
                do
                {
                    segmentEfforts = serv.Efforts(segmentID, offset: offset);
                } while (AddEfforts(ref rides, ref segmentEfforts, ref offset));

                rides.Sort();
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = originalCulture;
            }

            return rides;
        }

        private static bool AddEfforts(ref List<SegmentEffort> rides, ref SegmentEfforts segmentEfforts, ref int offset)
        {
            if (segmentEfforts.Efforts.Count == 0)
                return false;

            foreach (Stravan.Effort effort in segmentEfforts.Efforts)
            {
                offset++;
                var ride = new SegmentEffort()
                {
                    AthleteId = effort.Athlete.Id,
                    ElapsedTime = effort.ElapsedTime,
                    Id = effort.Id,
                    Start = Convert.ToDateTime(effort.StartDate),
                };
                rides.Add(ride);
                //Debug.WriteLine(String.Format("{0}, {1}, {2}, {3}", i, effort.Athlete.Name, effort.ElapsedTime, effort.StartDate.ToString()));
            }
            return true;
        }
    }
}