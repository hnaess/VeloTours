using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VeloTours.ViewModels;
using System.Diagnostics;
using VeloTours.Models;

namespace VeloTours.DAL.Shared
{
    public class RideAreaUpdate : RideUpdate
    {
        /// <summary>Worst time per ride</summary>
        public Dictionary<int, int> RidesWorstTime { get; protected set; }

        /// <summary>Leadboard per Ride per Athlete</summary>
        public Dictionary<int, Dictionary<int, LeaderBoard>> AthletesRidesLBoards { get; protected set; }

        protected Statistics info;
        protected decimal avgGradeTemp;

        protected double AverageGrade { get { return Convert.ToDouble((avgGradeTemp / Convert.ToDecimal(info.Distance))); } }

        public override void Update()
        {
            info.AvgGrade = AverageGrade;
            db.SaveChanges();

            AddResultAndLeadboards();
        }

        protected virtual void AddResultAndLeadboards()
        {
            throw new NotImplementedException();
        }

        protected void ResetCalcValues()
        {
            AthletesRidesLBoards = new Dictionary<int, Dictionary<int, LeaderBoard>>();
            RidesWorstTime = new Dictionary<int, int>();

            info.Distance = 0;
            info.ElevationGain = 0;
            avgGradeTemp = 0;
        }

        protected void AddStatData(Models.Statistics rideStatistic)
        {
            info.Distance += rideStatistic.Distance;
            info.ElevationGain += rideStatistic.ElevationGain;
            avgGradeTemp += Convert.ToDecimal(rideStatistic.Distance) * Convert.ToDecimal(rideStatistic.AvgGrade);
        }

        protected void AddToAthleteRidesLBoards(int athleteId, Dictionary<int, LeaderBoard> ridesLBoards)
        {
            foreach (var ridesLBoard in ridesLBoards)
            {
                AddToAthleteRidesLBoards(athleteId, ridesLBoard.Key, ridesLBoard.Value);
            }
        }

        protected void AddToAthleteRidesLBoards(int athleteId, int rideID, LeaderBoard segmentLBoard)
        {
            Dictionary<int, LeaderBoard> athleteLBoards;
            if (!AthletesRidesLBoards.ContainsKey(athleteId))
            {
                athleteLBoards = new Dictionary<int, LeaderBoard>();
                AthletesRidesLBoards.Add(athleteId, athleteLBoards);
            }
            else
            {
                athleteLBoards = AthletesRidesLBoards[athleteId];
            }
            athleteLBoards.Add(rideID, segmentLBoard);
        }

        protected void AddLeaderBoardsToResult(Models.Result dbResult)
        {
            var lBoardModels = GetLeaderBoard(dbResult);
            UpdateYerseysAndDefaultRank(dbResult, lBoardModels);

            lBoardModels.ForEach(x => db.LeaderBoards.Add(x));
            db.SaveChanges();
        }

        protected void UpdateYerseysAndDefaultRank(Result dbResult, List<LeaderBoard> lBoardModels)
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

        public List<Models.LeaderBoard> GetLeaderBoard(Models.Result dbResult)
        {
            int ridersRiddenAll = 0;
            var lBoardModels = new List<LeaderBoard>(AthletesRidesLBoards.Count);
            var yellowPtsWhenNotRidden = new Dictionary<int, int>();                        // Dictionary, to reduce performance bottel necks

            var debugAdded = new Dictionary<int, bool>();

            foreach (var athleteLBoards in AthletesRidesLBoards)
            {
                int athleteID = athleteLBoards.Key;

                int rideCount = 0;
                int rideMinCount = 0;
                int yellowPts = 0, greenPts = 0, polkaDotPts = 0;
                var elapsedTime = new ElapsedTimes();

                var rideList = RidesWorstTime.Keys;
                foreach (var rideID in rideList)
                {
                    var segmentBoards = athleteLBoards.Value;

                    var athleteRiddenSegment = segmentBoards.ContainsKey(rideID);
                    if (athleteRiddenSegment)
                    {
                        LeaderBoard leaderBoard = segmentBoards[rideID];
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
                        if (!yellowPtsWhenNotRidden.ContainsKey(rideID))
                        {
                            yellowPtsWhenNotRidden.Add(rideID, RidesWorstTime[rideID]);
                        }
                        elapsedTime.AddElapsedTimes(yellowPtsWhenNotRidden[rideID]);
                    }
                }

                #region debug
                if (debugAdded.ContainsKey(athleteID))
                    Debug.Fail("Shouldn't happen");
                else
                    debugAdded.Add(athleteID, true);

                #endregion

                int noRidden = 0;
                if (rideCount == RidesWorstTime.Count)
                {
                    noRidden = rideMinCount;
                    ridersRiddenAll++;
                }


                bool enoughRidesToBeAdded = (rideCount > 1);
                if (enoughRidesToBeAdded)
                {
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
            }
            return lBoardModels;
        }
    }
}