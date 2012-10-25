using System;
using System.Collections.Generic;
using System.Linq;
using VeloTours.DAL.Area;
using VeloTours.DAL.Shared;

namespace VeloTours.DAL.Region
{
    public class RegionUpdate : RideAreaUpdate
    {
        private int regionID;
        private Models.Region dbRegion;

        public RegionUpdate(int regionID)
        {
            this.regionID = regionID;
            dbRegion = db.Regions.Find(regionID);
            Info = dbRegion.Info;
            rideType = RideType.Region;
        }

        public override bool Update()
        {
            if (dbRegion.SegmentAreas.Count == 0)
                return false;           
            
            ResetCalcValues();
            foreach (var area in dbRegion.SegmentAreas)
            {
                AreaUpdate areaUpdate = new AreaUpdate(area.SegmentAreaID) { StravaWebClientObj = StravaWebClientObj };
                if (areaUpdate.Update())
                {
                    RidesWorstTime = RidesWorstTime.Concat(areaUpdate.RidesWorstTime).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                    foreach (var athleteRidesLBoard in areaUpdate.AthletesRidesLBoards)
                    {
                        AddToAthleteRidesLBoards(athleteRidesLBoard.Key, athleteRidesLBoard.Value);
                    }

                    AddStatData(areaUpdate.Info);
                }
            }
            return base.Update();
        }

        protected override void AddResultAndLeadboards()
        {
            var dbResult = db.ResultSet.Add(new Models.Result { Region = dbRegion, LastUpdated = DateTime.Now });
            db.SaveChanges();

            AddLeaderBoardsToResult(dbResult);
        }
    }
}