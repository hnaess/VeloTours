using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeloTours.Models
{
    public partial class SegmentArea
    {
        public List<Models.LeaderBoard> UpdateLeaderBoardForArea(Models.Result dbResult, Dictionary<int, int> segmentNotRiddenTime, Dictionary<int, Dictionary<int, LeaderBoard>> athletesSegmentsLBoards)
        {
            int ridersRiddenAll = 0;
            var lBoardModels = new List<LeaderBoard>(athletesSegmentsLBoards.Count);
            var yellowPtsWhenNotRidden = new Dictionary<int, int>();                        // Dictionary, to reduce performance bottel necks

            foreach (var athleteLBoards in athletesSegmentsLBoards)
            {
                int athleteID = athleteLBoards.Key;

                int rideCount = 0;
                int rideMinCount = 0;
                int yellowPts = 0, greenPts = 0, polkaDotPts = 0;
                var elapsedTime = new ElapsedTimes();

                //foreach (var leaderBoard in athleteLBoards.Value)
                foreach (var segmentID in segmentNotRiddenTime.Keys)
                {
                    var segmentBoards = athleteLBoards.Value;
                    
                    var athleteRiddenSegment = segmentBoards.ContainsKey(segmentID);
                    if (athleteRiddenSegment)
                    {
                        LeaderBoard leaderBoard = segmentBoards[segmentID];
                        rideCount++;
                        if (rideMinCount == 0 || leaderBoard.NoRidden < rideMinCount)
                            rideMinCount = leaderBoard.NoRidden;

                        yellowPts += leaderBoard.YellowPoints;
                        greenPts += leaderBoard.GreenPoints;
                        polkaDotPts += leaderBoard.PolkaDotPoints;
                        elapsedTime.AddElapsedTimes(leaderBoard);
                    }
                    else
                    {
                        if (!yellowPtsWhenNotRidden.ContainsKey(segmentID))
                        {
                            yellowPtsWhenNotRidden.Add(segmentID, segmentNotRiddenTime[segmentID]); // CalculateYellowPtsNotRidden(dbResult));
                        }
                        yellowPts += yellowPtsWhenNotRidden[segmentID];
                    }
                }

                int noRidden = 0;
                if (rideCount == segmentNotRiddenTime.Count)
                {
                    noRidden = rideMinCount;
                    ridersRiddenAll++;
                }

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

        //public void UpdateAreaResultWithYerseys()
        //{
        //    Models.Result areaResult = this.Result;

        //    var gYersey = from sa in this, 
        //}

        ///// <summary>
        ///// Calculate yellow points, if user haven't ridden the ride
        ///// </summary>
        ///// <returns>yellow points</returns>
        //private int CalculateYellowPtsNotRidden(Result result)
        //{
        //    int maxElapsedTime = result.Efforts.Last().ElapsedTime; // TODO: Review, if "correct"
        //    return maxElapsedTime;
        //}

    }
}