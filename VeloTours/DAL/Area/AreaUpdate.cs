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

            Dictionary<int, List<Models.LeaderBoard>> athletesLeaderBoards = new Dictionary<int, List<LeaderBoard>>();

            Statistics info = dbArea.Info;
            List<int> ridesInScope = new List<int>();
            info.Distance = 0;
            info.ElevationGain = 0;
            decimal avgGradeTemp = 0;

            foreach (var segment in dbArea.Segments)
            {
                SegmentUpdate segmentUpdater = new SegmentUpdate(segment.SegmentID);
                segmentUpdater.StravaWebClientObj = StravaWebClientObj;

                ridesInScope.Add(segment.SegmentID);
                var segmentInfo = segmentUpdater.UpdateSegment();
                if (updateEfforts)
                {
                    segmentUpdater.UpdateEfforts(segmentInfo);

                    // Update Leaderboard (dictionary)
                    var segmentLboards = segmentUpdater.EffortUpdater.LeaderBoards;
                    foreach (var segmentLboard in segmentLboards)
                    {
                        int athleteId = segmentLboard.AthleteID;
                        List<LeaderBoard> athleteLBoards;
                        if (!athletesLeaderBoards.ContainsKey(athleteId))
                        {
                            athleteLBoards = new List<LeaderBoard>() { segmentLboard };
                            athletesLeaderBoards.Add(athleteId, athleteLBoards);
                        }
                        else
                        {
                            athletesLeaderBoards[athleteId].Add(segmentLboard);
                        }
                    }

                    // Update Area Info
                    info.NoRidden += segmentLboards.Count(); // verify it works as expected
                    //info.NoRiders =
                }

                info.Distance += segmentInfo.Info.Distance;
                info.ElevationGain += segmentInfo.Info.ElevationGain;
                avgGradeTemp += Convert.ToDecimal(segmentInfo.Info.Distance) * Convert.ToDecimal(segmentInfo.Info.AvgGrade);
            }

            info.AvgGrade = Convert.ToDouble((avgGradeTemp / Convert.ToDecimal(info.Distance)));
            db.SaveChanges();

            var dbResult = AddResultSet(dbArea);
            db.SaveChanges();

            UpdateLeaderBoardForArea(dbArea, dbResult.ResultID, ridesInScope, athletesLeaderBoards);
            db.SaveChanges();

            // TODO
            UpdateResultWithYersey(dbResult);
            db.SaveChanges();
        }

        private void UpdateResultWithYersey(Result dbResult)
        {
            //throw new NotImplementedException();
        }

        private void UpdateLeaderBoardForArea(SegmentArea dbArea, int resultID, 
                                              List<int> ridesInScope, Dictionary<int, List<LeaderBoard>> athletesLeaderBoards)
        {
            List<Models.LeaderBoard> lBoardModels = new List<LeaderBoard>(athletesLeaderBoards.Count);
            
            int ridersRiddenAll = 0;
            List<int> yellowPtsWhenNotRidden = new List<int>(ridesInScope.Count);

            foreach (var athleteLBoards in athletesLeaderBoards)
            {
                int athleteID = athleteLBoards.Key;
                
                int rideCount = 0;
                int rideMinCount = 0;
                int yellowPts = 0;
                int greenPts = 0;
                int polkaDotPts = 0;
                ElapsedTimes elapsedTime = new ElapsedTimes();

                foreach (var leaderBoard in athleteLBoards.Value)
                {
                    rideCount++;
                    if(leaderBoard.NoRidden > rideMinCount)
                        rideMinCount = leaderBoard.NoRidden;

                    int rideID = leaderBoard.Result.Segment.SegmentID;
                    if (!ridesInScope.Contains(rideID))
                    {
                        if (yellowPtsWhenNotRidden.Contains(rideID))
                        {
                            yellowPtsWhenNotRidden[rideID] = CalculateYellowPtsNotRidden(leaderBoard.Result);
                        }
                        yellowPts += yellowPtsWhenNotRidden[rideID];
                    }
                    else
                        yellowPts += leaderBoard.YellowPoints;
                    greenPts += leaderBoard.GreenPoints;
                    polkaDotPts += leaderBoard.PolkaDotPoints;

                    AddElapsedTimes(ref elapsedTime, leaderBoard);
                }

                int noRidden = 0;
                if (rideCount == ridesInScope.Count)
                {
                    noRidden = rideMinCount;
                    ridersRiddenAll++;
                }

                lBoardModels.Add(new LeaderBoard()
                {
                    ResultID = resultID,

                    AthleteID = athleteID,
                    ElapsedTimes = elapsedTime,
                    GreenPoints = greenPts,
                    PolkaDotPoints = polkaDotPts,
                    YellowPoints = yellowPts,
                    NoRidden = noRidden,
                });
            }
            lBoardModels.ForEach(x => db.LeaderBoards.Add(x));
        }

        private static void AddElapsedTimes(ref ElapsedTimes elapsedTime, LeaderBoard leaderBoard)
        {
            elapsedTime.Min += leaderBoard.ElapsedTimes.Min;
            elapsedTime.Max += leaderBoard.ElapsedTimes.Max;
            elapsedTime.Median += leaderBoard.ElapsedTimes.Median;
            elapsedTime.Average += leaderBoard.ElapsedTimes.Average;
            elapsedTime.Percentile90 += leaderBoard.ElapsedTimes.Percentile90;
            elapsedTime.Stdev += elapsedTime.Stdev;
        }

        
        /// <summary>
        /// Calculate yellow points, if user haven't ridden the ride
        /// </summary>
        /// <returns>yellow points</returns>
        private int CalculateYellowPtsNotRidden(Result result)
        {
            int maxElapsedTime = result.Efforts.Last().ElapsedTime; // TODO: Review, if "correct"
            return maxElapsedTime;
        }

        private Result AddResultSet(Models.SegmentArea dbArea)
        {
            var result = new Models.Result { SegmentArea = dbArea, LastUpdated = DateTime.Now };
            result = db.ResultSet.Add(result);
            return result;
        }
    }
}