using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using VeloTours.Models;

namespace VeloTours.ViewModels
{
    public class SegmentViewModel : Ride
    {
        #region Properties

        // Navigators
        public ICollection<SegmentArea> SegmentAreas { get; set; }

        // Model
        public Models.Segment Segment { get; set; }
        public override Statistics Info { get { return Segment.Info; } }
        public string ImprovementHint { get; set; }
        
        #endregion

        public bool IsClimb { get { return IsClimbCat(Info.ClimbCategory); } }
        public static bool IsClimbCat(string climbCategory) { return climbCategory != "NC"; }

        #region Constructors

        public SegmentViewModel(int athleteID, Models.Segment dbSegment, int? leaderBoardPageNo)
        {
            Segment = dbSegment;
            SegmentAreas = dbSegment.SegmentAreas;
            var dbResult = dbSegment.Result;

            if (SetRide(athleteID, leaderBoardPageNo, dbResult))
            {
                ImprovementHint = RideUtil.CreateImprovementHint(Athlete.ElapsedTimes.Min, dbResult.LeaderBoards, lBoardAthlete);
            }
        }

        #endregion
    }
}
