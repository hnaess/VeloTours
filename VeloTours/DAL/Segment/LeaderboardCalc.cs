using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeloTours.DAL.Segment
{
    public class LeaderboardCalc
    {
        internal static int CalcYellowPoints(int elapsedTimeKOM, int eleapsedTime)
        {
            return eleapsedTime - elapsedTimeKOM;
        }

        internal static int CalcPolkaDotPoints(int rank, int riders, string climbCategory)
        {
            int points = 0;
            if (IsClimb(climbCategory))
                points = PointsPerRank(rank, riders);

            return points;
        }

        internal static int CalcGreenPoints(int rank, int riders, string climbCategory)
        {
            int points = 0;
            if (!IsClimb(climbCategory))
                points = PointsPerRank(rank, riders);

            return points;
        }

        internal static bool IsClimb(string climbCategory)
        {
            return !climbCategory.Equals("NC");
        }

        /// <summary>
        /// Calculate points. 50 is max score (if more >=50 riders), if less riders than subtract with difference between max score and number of riders
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="riders"></param>
        /// <returns></returns>
        private static int PointsPerRank(int rank, int riders)
        {
            int maxPoints = 50;
            if (riders < maxPoints)
                maxPoints -= (maxPoints - riders);

            int points = maxPoints - rank + 1;
            return points > 0 ? points : 0;
        }

        public static int VAM(int seconds, double elevation)
        {
            int vam = Convert.ToInt16(Math.Round(elevation * 3600 / seconds));
            return vam;
        }
    }
}