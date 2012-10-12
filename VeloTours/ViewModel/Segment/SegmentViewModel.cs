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
        
        //public ICollection<Models.LeaderBoard> LeaderBoard { get { return Segment.Result.LeaderBoards; } }
        public IPagedList<Models.LeaderBoard> LeaderBoard { get; set; }

        public double CalculateSpeed(Models.LeaderBoard leaderBoard)
        {
            return (this.Segment.Info.Distance / leaderBoard.ElapsedTimes.Min) * 3.6;
        }
    }
}
