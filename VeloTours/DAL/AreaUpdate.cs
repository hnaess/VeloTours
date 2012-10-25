using System;
using System.Collections.Generic;
using System.Linq;
using VeloTours.Models;
using VeloTours.DAL.Segment;
using VeloTours.DAL.Shared;
using VeloTours.ViewModels;
using System.Diagnostics;

namespace VeloTours.DAL.Area
{
    public class AreaUpdate : RideAreaUpdate
    {
        private int areaID; 
        private Models.SegmentArea dbArea;

        public AreaUpdate(int areaID)
        {
            this.areaID = areaID;
            dbArea = db.SegmentAreas.Find(areaID);
            info = dbArea.Info;
            rideType = RideType.Area;
        }

        public override bool Update()
        {
            if (dbArea.Segments.Count == 0)
                return false;

            ResetCalcValues();
            foreach (var segment in dbArea.Segments)
            {
                SegmentUpdate segmentUpdater = new SegmentUpdate(segment.SegmentID) { StravaWebClientObj = StravaWebClientObj };

                var segmentStat = segmentUpdater.UpdateSegment();
                UpdateEffortOnSegment(segment.SegmentID, segmentStat, segmentUpdater);

                AddStatData(segmentStat.Info);
            }
            return base.Update();
        }

        protected override void AddResultAndLeadboards()
        {
            var dbResult = db.ResultSet.Add(new Models.Result { SegmentArea = dbArea, LastUpdated = DateTime.Now });
            db.SaveChanges();

            AddLeaderBoardsToResult(dbResult);
        }

        private void UpdateEffortOnSegment(int segmentID, Models.Segment segmentInfo, SegmentUpdate segmentUpdater)
        {
            segmentUpdater.UpdateEfforts(segmentInfo);
            RidesWorstTime.Add(segmentID, segmentUpdater.WorstPlacing);

            // Update Leaderboard (dictionary)
            var segmentLBoards = segmentUpdater.LeaderBoards;
            foreach (var segmentLBoard in segmentLBoards)
            {
                AddToAthleteRidesLBoards(segmentLBoard.AthleteID, segmentID, segmentLBoard);
            }

            // Update Info
            info.NoRidden += segmentLBoards.Count(); // verify it works as expected
            //info.NoRiders =
        }
    }
}