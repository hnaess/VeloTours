using System;
using System.Collections.Generic;
using PagedList;

namespace VeloTours.Models
{
    public class SegmentAreaViewModel
    {
        public Models.SegmentArea SegmentArea { get; set; }
        public Nullable<double> KomSpeed { get; set; }
        public AthleteRideInfo Athlete { get; set; }

        public List<SegmentViewModel> Segments { get; set; }
        
        public Statistics Info { get { return SegmentArea.Info; } }
        //public ICollection<Models.LeaderBoard> LeaderBoard { get { return SegmentArea.Result != null ? SegmentArea.Result.LeaderBoards : null; } }
        public IPagedList<Models.LeaderBoard> LeaderBoard { get; set; } 

        public LeaderBoard YellowYersey { get; set; }
        public LeaderBoard GreenYersey { get; set; }
        public LeaderBoard PolkaDotYersey { get; set; }
    }
}