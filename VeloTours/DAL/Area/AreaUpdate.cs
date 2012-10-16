using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Stravan;
using Stravan.Json;
using VeloTours.Models;
using VeloTours.DAL.Segment;
using VeloTours.ViewModel;

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

            var athletesSegmentsLBoards = new Dictionary<int, Dictionary<int, LeaderBoard>>();      // <athleteId, <segmentID, LeaderBoard-ref> >

            var info = dbArea.Info;
            var segmentNotRiddenTime = new Dictionary<int, int>();
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
                    segmentNotRiddenTime.Add(segment.SegmentID, segmentUpdater.WorstPlacing);

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
                        athleteLBoards.Add(segment.SegmentID, segmentLBoard);
                    }

                    // Update Area Info
                    info.NoRidden += segmentLBoards.Count(); // verify it works as expected
                    //info.NoRiders =
                }

                info.Distance += segmentInfo.Info.Distance;
                info.ElevationGain += segmentInfo.Info.ElevationGain;
                avgGradeTemp += Convert.ToDecimal(segmentInfo.Info.Distance) * Convert.ToDecimal(segmentInfo.Info.AvgGrade);
            }
            info.AvgGrade = Convert.ToDouble((avgGradeTemp / Convert.ToDecimal(info.Distance)));
            db.SaveChanges();

            if (updateEfforts)
            {
                var dbResult = db.ResultSet.Add(new Models.Result { SegmentArea = dbArea, LastUpdated = DateTime.Now });
                db.SaveChanges();

                var lBoardModels = dbArea.UpdateLeaderBoardForArea(dbResult, segmentNotRiddenTime, athletesSegmentsLBoards);
                lBoardModels.ForEach(x => db.LeaderBoards.Add(x));
                db.SaveChanges();

                dbResult.GreenYerseyLB = db.LeaderBoards.OrderByDescending(x => x.GreenPoints).First();
                dbResult.YellowYerseyLB = db.LeaderBoards.OrderByDescending(x => x.YellowPoints).First();
                dbResult.PolkaDotYerseyLB = db.LeaderBoards.OrderByDescending(x => x.PolkaDotPoints).First();
                db.SaveChanges();

                //dbArea.UpdateAreaResultWithYerseys();
                //db.SaveChanges();
            }
        }
    }
}