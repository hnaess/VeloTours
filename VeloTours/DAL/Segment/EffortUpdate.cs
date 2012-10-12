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

        private List<Models.Effort> Efforts;

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
            throw new NotImplementedException("Update based on dates");
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
      
        internal EffortUpdateStatus UpdateLeaderboard(string segmentClimbCategory)
        {
            List<Models.LeaderBoard> leaderBoards;
            Dictionary<int, List<int>> athleteEffortsList;

            EffortUpdateStatus rideInfo = 
                GetSortedAthleteEfforts(out leaderBoards, out athleteEffortsList);
            
            UpdateLeaderBoard(segmentClimbCategory, leaderBoards, athleteEffortsList, rideInfo);

            var komAthlete = leaderBoards.First();
            UpdateResultTableWithPoints(komAthlete);

            return rideInfo;
        }

        private void UpdateResultTableWithPoints(LeaderBoard komAthlete)
        {
            int i = 1;
            //db.ResultSet.Add(dbResult);
            //db.SaveChanges();
        }

        private void UpdateLeaderBoard(string segmentClimbCategory, List<Models.LeaderBoard> leaderBoards, Dictionary<int, List<int>> athleteEffortsList, EffortUpdateStatus rideInfo)
        {
            int? elapsedTimeKOM = null;
            foreach (var l in leaderBoards)
            {
                List<int> effort = athleteEffortsList[l.AthleteID];
                effort.Sort(); // TODO: Review, need this - isn't it already sorted?

                double stdev = effort.StandardDeviation();
                l.NoRidden = effort.Count;
                l.ElapsedTimes = new ElapsedTimes
                {
                    Average = (int)effort.Average(),
                    Max = effort.Max(),
                    Min = effort.Min(),
                    Median = (int)effort.Median(),
                    Percentile90 = effort[Convert.ToInt32(Math.Round(effort.Count * 0.10))],
                    Stdev = Double.IsNaN(stdev) ? 1 : stdev,
                };

                if (elapsedTimeKOM == null)
                    elapsedTimeKOM = l.ElapsedTimes.Min;

                l.YellowPoints = LeaderboardCalc.CalcYellowPoints((int)elapsedTimeKOM, (int)l.ElapsedTimes.Min);
                l.GreenPoints = LeaderboardCalc.CalcGreenPoints(l.Rank, rideInfo.riders, segmentClimbCategory);
                l.PolkaDotPoints = LeaderboardCalc.CalcPolkaDotPoints(l.Rank, rideInfo.riders, segmentClimbCategory);
            }

            leaderBoards.ForEach(items => db.LeaderBoards.Add(items));
            db.SaveChanges();
        }

        private EffortUpdateStatus GetSortedAthleteEfforts(out List<Models.LeaderBoard> leaderBoards, out Dictionary<int, List<int>> athleteEffortsList)
        {
            leaderBoards = new List<Models.LeaderBoard>();
            athleteEffortsList = new Dictionary<int, List<int>>();

            var sortedEfforts =
                from n in db.Efforts
                where n.ResultID == dbResult.ResultID
                orderby n.ElapsedTime ascending, n.AthleteID  
                select new { n.AthleteID, n.ElapsedTime };

            int rank = 0;
            int rides = 0;
            int prevDuration = 0;
            foreach (var e in sortedEfforts)
            {
                rides++;
                List<int> athleteEfforts;
                athleteEffortsList.TryGetValue(e.AthleteID, out athleteEfforts);
                if (athleteEfforts == null)
                {
                    if (prevDuration != e.ElapsedTime)
                        rank = athleteEffortsList.Count + 1;

                    athleteEfforts = new List<int>();
                    athleteEffortsList[e.AthleteID] = athleteEfforts;

                    leaderBoards.Add(new Models.LeaderBoard() { AthleteID = e.AthleteID, Rank = rank, Result = dbResult });
                    prevDuration = e.ElapsedTime;
                }
                athleteEfforts.Add(e.ElapsedTime);
            }
            return new EffortUpdateStatus(athleteEffortsList.Count, rides);
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
                    VAM = LeaderboardCalc.VAM(effort.ElapsedTime, elevationGain),
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
    }
}