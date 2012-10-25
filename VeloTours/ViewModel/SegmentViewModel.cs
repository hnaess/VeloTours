using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using VeloTours.Models;

namespace VeloTours.ViewModels
{
    public class SegmentViewModel : RideViewModel
    {
        #region Properties

        // Navigators
        public ICollection<SegmentArea> SegmentAreas { get; set; }

        // Model
        public Models.Segment Segment { get; set; }
        public override Statistics Info { get { return Segment.Info; } }
        public override int RideID { get { return Segment.SegmentID; } }
        public string ImprovementHint { get; set; }
        
        #endregion

        public bool IsClimb { get { return IsClimbCat(Info.ClimbCategory); } }
        public static bool IsClimbCat(string climbCategory) { return climbCategory != "NC"; }
        public override bool IsMTB { get { return false; } }

        #region Constructors

        public SegmentViewModel(int athleteID, Models.Segment dbSegment, int? leaderBoardPageNo)
        {
            Segment = dbSegment;
            SegmentAreas = dbSegment.SegmentAreas;
            var dbResult = dbSegment.Result;

            if (SetRide(athleteID, leaderBoardPageNo, dbResult))
            {
                ImprovementHint = CreateImprovementHint(Athlete.ElapsedTimes.Min, dbResult.LeaderBoards, lBoardAthlete);
            }
        }

        #endregion

        internal static string CreateImprovementHint(int duration, ICollection<LeaderBoard> lBoard, Models.LeaderBoard lAthlete)
        {
            string hint = String.Empty;
            if (lAthlete != null)
            {
                hint += CreateImprovementHintElement(duration < 500 ? 3 : 10, duration, lBoard, lAthlete.Rank);
                hint += CreateImprovementHintElement(duration < 500 ? 10 : 30, duration, lBoard, lAthlete.Rank);
            }
            return hint;
        }

        private static string CreateImprovementHintElement(int seconds, int duration, ICollection<LeaderBoard> lBoard, int rank)
        {
            int newRank = 0;
            var improveAquery = (from lb in lBoard where lb.ElapsedTimes.Min <= (duration - seconds) select lb.Rank);
            if (improveAquery.Count() > 0)
                newRank = improveAquery.Last();

            int rankImprovement = rank - newRank - 1;

            if (rankImprovement > 0) // TODO: Verify rank correct
                return String.Format("Ride {0} seconds faster, improve {1} position to #{2}. ", seconds, rankImprovement, newRank + 1);

            return string.Empty;
        }
    }
}
