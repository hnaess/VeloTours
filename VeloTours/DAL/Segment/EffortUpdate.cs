﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using Stravan;
using Stravan.Json;
using VeloTours.DAL.Athlete;
using VeloTours.Models;
using LinqStatistics;

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

        public List<Models.Effort> Efforts { get; private set; }

        #region Constructors

        public EffortUpdate(TourModelContainer db, Models.Result dbResult, int segmentID, double elevationGain)
        {
            this.db = db;
            this.dbResult = dbResult;
            this.segmentID = segmentID;
            this.elevationGain = elevationGain;
        }

        #endregion

        internal void UpdateEfforts(DateTime startDate, DateTime endDate)
        {

        }



        internal void UpdateEfforts()
        {
            ImportEffortsFromStrava();
            
            // TODO: Delete previous results
            if (Efforts.Count == 0)
            {
                // Marked as hazardious ?
                throw new NotSupportedException("Not expected");
            }           
        }

        internal void UpdateLeaderboard()
        {
            List<Models.LeaderBoard> leaderBoards = new List<Models.LeaderBoard>();

            Models.Segment dbSegments = db.Segments.Find(segmentID);

            Dictionary<int, List<int>> athleteEffortsList = new Dictionary<int, List<int>>();
            int rank = 0;
            foreach (var e in Efforts)
            {
                List<int> athleteEfforts;
                athleteEffortsList.TryGetValue(e.AthleteID, out athleteEfforts);
                if (athleteEfforts == null)
                {
                    rank++;                    
                    athleteEfforts = new List<int>();
                    athleteEffortsList[e.AthleteID] = athleteEfforts;

                    leaderBoards.Add(new Models.LeaderBoard() { AthleteID = e.AthleteID, Rank = rank, Result = dbResult });
                }
                athleteEfforts.Add(e.ElapsedTime);
            }

            foreach(var l in leaderBoards)
            {
                int athleteID = l.AthleteID;
                List<int> effort = athleteEffortsList[athleteID];
                effort.Sort();

                double stdev = effort.StandardDeviation();

                l.NoRidden = effort.Count;
                l.ElapsedTimes = new ElapsedTimes
                {
                    Average = (int)effort.Average(),
                    Max = effort.Max(),
                    Min = effort.Min(),
                    Median = (int)effort.Median(),
                    Quartile = effort[Convert.ToInt32(Math.Round(effort.Count * 0.10))],
                    Stdev = Double.IsNaN(stdev) ? 1 : stdev,
                };
                l.YellowPoints = CalcYellowPoints(leaderBoards.First().ElapsedTimes.Min, l.ElapsedTimes.Min);
                l.GreenPoints = CalcGreenPoints(l.Rank, dbSegments.ClimbCategory);
                l.PolkaDotPoints = CalcPolkaDotPoints(l.Rank, dbSegments.ClimbCategory);
            }
            
            leaderBoards.ForEach(items => db.LeaderBoards.Add(items));
            db.SaveChanges();
        }

        private void ImportEffortsFromStrava()
        {
            Efforts = new List<Models.Effort>();
            var originalCulture = Utils.SetStravaCultureAndReturnCurrentCulture();
            try
            {
                SegmentService serv = new SegmentService(StravaWebClientObj);
                Stravan.Segment segmentinfo = serv.Show(segmentID);

                GetEfforts(serv);
                AthleteUpdate.UpdateAthlete();
                SaveEfforts();
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = originalCulture;
            }
        }

        private void SaveEfforts()
        {
            Efforts.ForEach(effort => db.Efforts.Add(effort));
            db.SaveChanges();
        }

        private void GetEfforts(SegmentService serv)
        {
            SegmentEfforts stravaEfforts;
            int offset = 0;
            do
            {
                stravaEfforts = serv.Efforts(segmentID, offset: offset);
            } while (GetEffortsLoop(ref stravaEfforts, ref offset));
        }

        private bool GetEffortsLoop(ref SegmentEfforts segmentEfforts, ref int offset)
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

                Efforts.Add(dbEffort);
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