using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using Stravan;
using Stravan.Json;
using VeloTours.DAL.Athlete;
using VeloTours.Models;

namespace VeloTours.DAL.Segment
{
    public class EffortUpdate
    {
        #region Singletons

        private StravaWebClient _stravaWebClient;
        public StravaWebClient StravaWebClientObj
        {
            get { return _stravaWebClient ?? (_stravaWebClient = new StravaWebClient()); }
            set { _stravaWebClient = value; }
        }

        #endregion

        private TourModelContainer db;
        private Models.Result dbResult;
        private int segmentID;
        private double elevationGain;

        #region Constructors

        public EffortUpdate(TourModelContainer db, Models.Result dbResult, int segmentID, double elevationGain)
        {
            this.db = db;
            this.dbResult = dbResult;
            this.segmentID = segmentID;
            this.elevationGain = elevationGain;
        }

        #endregion

        internal void UpdateEfforts()
        {
            List<Models.Effort> stravaEfforts = ImportEffortsFromStrava();
            
            // TODO: Delete previous results
            if (stravaEfforts.Count == 0)
            {
                // Marked as hazardious ?
                throw new NotSupportedException("Not expected");
            }           
        }

        private List<Models.Effort> ImportEffortsFromStrava()
        {
            List<Models.Effort> efforts = null;
            var originalCulture = Utils.SetStravaCultureAndReturnCurrentCulture();
            try
            {
                SegmentService serv = new SegmentService(StravaWebClientObj);
                Stravan.Segment segmentinfo = serv.Show(segmentID);

                efforts = GetEfforts(serv);
                AthleteUpdate.UpdateAthlete();
                SaveEfforts(efforts);
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = originalCulture;
            }

            return efforts;
        }

        private void SaveEfforts(List<Models.Effort> efforts)
        {
            // Saving
            efforts.ForEach(effort => db.Efforts.Add(effort));
            db.SaveChanges();
        }

        private List<Models.Effort> GetEfforts(SegmentService serv)
        {
            List<Models.Effort> efforts = new List<Models.Effort>();
            SegmentEfforts stravaEfforts;
            int offset = 0;
            do
            {
                stravaEfforts = serv.Efforts(segmentID, offset: offset);
            } while (GetEffortsLoop(ref efforts, ref stravaEfforts, ref offset));
            return efforts;
        }

        private bool GetEffortsLoop(ref List<Models.Effort> rides, ref SegmentEfforts segmentEfforts, ref int offset)
        {
            if (segmentEfforts.Efforts.Count == 0)
                return false;

            foreach (Stravan.Effort effort in segmentEfforts.Efforts)
            {
                offset++;
                var dbEffort = new Models.Effort
                {
                    Result = dbResult,
                    AthleteID = effort.Athlete.Id,
                    EffortID = effort.Id,
                    ElapsedTime = effort.ElapsedTime,
                    StartDate =  DateTime.Parse(effort.StartDate, new System.Globalization.CultureInfo("en-US")),
                    StravaActivityID = Convert.ToInt32(effort.ActivityId),
                    StravaID = effort.Id,
                    VAM = CaclculateVAM(effort.ElapsedTime, elevationGain),
                };
                EnsureSavingOfAthlete(effort.Athlete.Name, dbEffort.AthleteID);

                rides.Add(dbEffort);
                //Debug.WriteLine(String.Format("{0}, {1}, {2}, {3}", i, effort.Athlete.Name, effort.ElapsedTime, effort.StartDate.ToString()));
            }
            return true;
        }

        /// <summary>
        /// Add the athlete the to cache, will be used for savling later.
        /// </summary>
        private static void EnsureSavingOfAthlete(string athleteName, int athleteID)
        {
            bool updateUsers = false;
            AthleteShortInfo cacheItem;
            AthleteUpdate.Athletes.TryGetValue(athleteID, out cacheItem);
            if (updateUsers && cacheItem.State == AthleteShortInfo.DbState.Saved)
            {
                cacheItem.Name = athleteName;
                cacheItem.State = AthleteShortInfo.DbState.Update;
            }
            if (cacheItem == null)
            {
                AthleteUpdate.Athletes[athleteID] = new AthleteShortInfo() { Name = athleteName };
            }
        }

        private static int CaclculateVAM(int seconds, double elevation)
        {
            int vam = Convert.ToInt16(Math.Round(elevation * 3600 / seconds));
            return vam;
        }
    }
}