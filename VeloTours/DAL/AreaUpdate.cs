using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeloTours.Models;
using VeloTours.DAL.Segment;
using VeloTours.ViewModels;
using System.Diagnostics;

namespace VeloTours.DAL.Area
{
    public class AreaUpdate : RideUpdate
    {
        private int areaID; 
        private Models.SegmentArea dbArea;
        private Statistics info;
        private decimal avgGradeTemp;

        /// <summary>Leadboard per Segment per Athlete</summary>
        private Dictionary<int, Dictionary<int, LeaderBoard>> athletesSegmentsLBoards;
        
        /// <summary>Worst time per segment</summary>
        private Dictionary<int, int> segmentsWorstTime;

        public AreaUpdate(int areaID)
        {
            this.areaID = areaID;
            dbArea = db.SegmentAreas.Find(areaID);
            info = dbArea.Info;
        }

        public override void Update()
        {
            if (dbArea.Segments.Count == 0)
                return; // TODO: Logging?

            athletesSegmentsLBoards = new Dictionary<int, Dictionary<int, LeaderBoard>>();
            segmentsWorstTime = new Dictionary<int, int>();

            info = dbArea.Info;
            info.Distance = 0;
            info.ElevationGain = 0;
            avgGradeTemp = 0;

            foreach (var segment in dbArea.Segments)
            {
                SegmentUpdate segmentUpdater = new SegmentUpdate(segment.SegmentID);
                segmentUpdater.StravaWebClientObj = StravaWebClientObj;

                var segmentInfo = segmentUpdater.UpdateSegment();
                UpdateEffortOnSegment(segment.SegmentID, segmentInfo, segmentUpdater);

                info.Distance += segmentInfo.Info.Distance;
                info.ElevationGain += segmentInfo.Info.ElevationGain;
                avgGradeTemp += Convert.ToDecimal(segmentInfo.Info.Distance) * Convert.ToDecimal(segmentInfo.Info.AvgGrade);
            }
            info.AvgGrade = Convert.ToDouble((avgGradeTemp / Convert.ToDecimal(info.Distance)));
            db.SaveChanges();

            AddResultAndLeadboards();
        }

        private void AddResultAndLeadboards()
        {
            var dbResult = db.ResultSet.Add(new Models.Result { SegmentArea = dbArea, LastUpdated = DateTime.Now });
            db.SaveChanges();

            var lBoardModels = GetLeaderBoardForArea(dbResult);
            UpdateYerseysAndDefaultRank(dbResult, lBoardModels);

            lBoardModels.ForEach(x => db.LeaderBoards.Add(x));
            db.SaveChanges();
        }

        //private void SetYerseys(Result dbResult, List<LeaderBoard> lBoardModels)
        //{
        //    var yellow = lBoardModels.OrderBy(x => x.ElapsedTimes.Min);
        //    foreach (var y in yellow)
        //    {
        //        Debug.WriteLine(y.Athlete.Name + " - " + y.ElapsedTimes.Min + " - " + y.YellowPoints);
        //    }
        //    dbResult.YellowYerseyLB = yellow.First();

        //    dbResult.GreenYerseyLB = lBoardModels.OrderByDescending(x => x.GreenPoints).First();
        //    dbResult.PolkaDotYerseyLB = lBoardModels.OrderByDescending(x => x.PolkaDotPoints).First();
        //}

        private void UpdateYerseysAndDefaultRank(Result dbResult, List<LeaderBoard> lBoardModels)
        {
            var sortedlBoardModels = lBoardModels.OrderBy(x => x.ElapsedTimes.Min);
            var lBoardKOM = sortedlBoardModels.First();
            int elapsedTimeKom = lBoardKOM.ElapsedTimes.Min;

            dbResult.YellowYerseyLB = lBoardKOM;
            dbResult.GreenYerseyLB = lBoardModels.OrderByDescending(x => x.GreenPoints).First();
            dbResult.PolkaDotYerseyLB = lBoardModels.OrderByDescending(x => x.PolkaDotPoints).First();

            int rank = 0;            
            int prevDuration = 0;
            int i = 0;
            foreach (var leaderBoard in sortedlBoardModels)
            {
                i++;

                var elapsedTime = leaderBoard.ElapsedTimes.Min;
                if (prevDuration != elapsedTime)
                    rank = i;

                leaderBoard.Rank = rank;
                leaderBoard.YellowPoints = elapsedTime - elapsedTimeKom;

                prevDuration = elapsedTime;
            }
        }

        private void UpdateEffortOnSegment(int segmentID, Models.Segment segmentInfo, SegmentUpdate segmentUpdater)
        {
            segmentUpdater.UpdateEfforts(segmentInfo);
            segmentsWorstTime.Add(segmentID, segmentUpdater.WorstPlacing);

            // Update Leaderboard (dictionary)
            var segmentLBoards = segmentUpdater.LeaderBoards;
            foreach (var segmentLBoard in segmentLBoards)
            {
                int athleteId = segmentLBoard.AthleteID;

                Dictionary<int, LeaderBoard> athleteLBoards;
                if (!athletesSegmentsLBoards.ContainsKey(athleteId))
                {
                    athleteLBoards = new Dictionary<int, LeaderBoard>();
                    athletesSegmentsLBoards.Add(athleteId, athleteLBoards);
                }
                else
                {
                    athleteLBoards = athletesSegmentsLBoards[athleteId];
                }
                athleteLBoards.Add(segmentID, segmentLBoard);
            }

            // Update Area Info
            info.NoRidden += segmentLBoards.Count(); // verify it works as expected
            //info.NoRiders =
        }

        public List<Models.LeaderBoard> GetLeaderBoardForArea(Models.Result dbResult)
        {
            int ridersRiddenAll = 0;
            var lBoardModels = new List<LeaderBoard>(athletesSegmentsLBoards.Count);
            var yellowPtsWhenNotRidden = new Dictionary<int, int>();                        // Dictionary, to reduce performance bottel necks

            var debugAdded = new Dictionary<int, bool>();

            foreach (var athleteLBoards in athletesSegmentsLBoards)
            {
                int athleteID = athleteLBoards.Key;

                int rideCount = 0;
                int rideMinCount = 0;
                int yellowPts = 0, greenPts = 0, polkaDotPts = 0;
                var elapsedTime = new ElapsedTimes();

                //foreach (var leaderBoard in athleteLBoards.Value)
                foreach (var segmentID in segmentsWorstTime.Keys)
                {
                    var segmentBoards = athleteLBoards.Value;

                    var athleteRiddenSegment = segmentBoards.ContainsKey(segmentID);
                    if (athleteRiddenSegment)
                    {
                        LeaderBoard leaderBoard = segmentBoards[segmentID];
                        rideCount++;
                        if (rideMinCount == 0 || leaderBoard.NoRidden < rideMinCount)
                            rideMinCount = leaderBoard.NoRidden;

                        //yellowPts += leaderBoard.YellowPoints;    Not Correct because leader will not be 00:00 (if not KOM on all)
                        greenPts += leaderBoard.GreenPoints;
                        polkaDotPts += leaderBoard.PolkaDotPoints;
                        elapsedTime.AddElapsedTimes(leaderBoard);
                    }
                    else
                    {
                        if (!yellowPtsWhenNotRidden.ContainsKey(segmentID))
                        {
                            yellowPtsWhenNotRidden.Add(segmentID, segmentsWorstTime[segmentID]);
                        }
                        elapsedTime.AddElapsedTimes(yellowPtsWhenNotRidden[segmentID]);
                    }
                }

                int noRidden = 0;
                if (rideCount == segmentsWorstTime.Count)
                {
                    noRidden = rideMinCount;
                    ridersRiddenAll++;
                }

                #region debug
                if (debugAdded.ContainsKey(athleteID))
                    Debug.Fail("Shouldn't happem");
                else
                    debugAdded.Add(athleteID, true);

                #endregion

                lBoardModels.Add(new LeaderBoard()
                {
                    ResultID = dbResult.ResultID,

                    AthleteID = athleteID,
                    ElapsedTimes = elapsedTime,
                    GreenPoints = greenPts,
                    PolkaDotPoints = polkaDotPts,
                    YellowPoints = yellowPts,
                    NoRidden = noRidden,
                });
            }
            return lBoardModels;
        }


    }
}