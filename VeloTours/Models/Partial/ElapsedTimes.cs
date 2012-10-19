using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeloTours.Models
{
    public partial class ElapsedTimes
    {
        public void AddElapsedTimes(LeaderBoard leaderBoard)
        {
            Min += leaderBoard.ElapsedTimes.Min;
            Max += leaderBoard.ElapsedTimes.Max;
            Median += leaderBoard.ElapsedTimes.Median;
            Average += leaderBoard.ElapsedTimes.Average;
            Percentile90 += leaderBoard.ElapsedTimes.Percentile90;
            Stdev += leaderBoard.ElapsedTimes.Stdev;
        }

        public void AddElapsedTimes(int time)
        {
            Min += time;
            Max += time;
            Median += time;
            Average += time;
            Percentile90 += time;
            Stdev += 0;
        }
    }
}