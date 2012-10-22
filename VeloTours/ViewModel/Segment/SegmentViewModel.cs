using System;
using System.Collections.Generic;
using PagedList;

namespace VeloTours.Models
{
    public class SegmentViewModel
    {
        public Models.Segment Segment { get; set; }
        public Nullable<double> KomSpeed { get; set; }
        public AthleteRideInfo Athlete { get; set; }

        public string ImprovementHint { get; set; }

        public Statistics Info { get { return Segment.Info; } }
        public IPagedList<Models.LeaderBoard> LeaderBoard { get; set; }

        public LeaderBoard YellowYersey { get; set; }
        public LeaderBoard GreenYersey { get; set; }
        public LeaderBoard PolkaDotYersey { get; set; }

        public ICollection<SegmentArea> SegmentAreas { get; set; }

        public double CalculateSpeed(Models.LeaderBoard leaderBoard)
        {
            return (this.Segment.Info.Distance / leaderBoard.ElapsedTimes.Min) * 3.6;
        }

        public bool IsClimb { get { return IsClimbCat(Info.ClimbCategory); } }

        public static bool IsClimbCat(string climbCategory)
        {
            return climbCategory != "NC";
        }

        public String KomSpeedString { get { return (KomSpeed == null) ? null : String.Format("{0:#0.0}", KomSpeed); } }
    }
}
