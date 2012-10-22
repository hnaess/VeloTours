using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using PagedList;
using VeloTours.Models;

namespace VeloTours.ViewModels
{
    public static class RideUtil
    {
        //private string DefaultLBoardSortOrder = "Min";
        private static int pageSize = 20;
        //string lbSortOrder = null;

        internal static IPagedList<LeaderBoard> CreateBlankPagedList()
        {
            return new List<Models.LeaderBoard>().ToPagedList(1, 1);
        }

        internal static AthleteRideInfo CreateBlankAthleteInfo()
        {
            return new AthleteRideInfo();
        }

        internal static AthleteRideInfo AddAthleteToViewModel(int athleteID, ICollection<LeaderBoard> leaderBoards, Models.LeaderBoard lBoardKOM, Models.LeaderBoard lBoardAthlete, int noRiders)
        {
            AthleteRideInfo athleteRideInfo = new AthleteRideInfo();
            if (lBoardAthlete != null)
            {
                athleteRideInfo.BehindKom = lBoardAthlete.ElapsedTimes.Min - lBoardKOM.ElapsedTimes.Min;
                athleteRideInfo.BehindKomPercentage = ((double)lBoardAthlete.ElapsedTimes.Min / (double)lBoardKOM.ElapsedTimes.Min - 1) * 100;
                athleteRideInfo.ElapsedTimes = lBoardAthlete.ElapsedTimes;
                athleteRideInfo.NoRidden = lBoardAthlete.NoRidden;
                athleteRideInfo.Position = lBoardAthlete.Rank;
                athleteRideInfo.PositionPercentage = ((double)lBoardAthlete.Rank / (double)noRiders * 100);

                athleteRideInfo.Green = lBoardAthlete.GreenPoints;
                athleteRideInfo.PolkaDot = lBoardAthlete.PolkaDotPoints;
            }
            return athleteRideInfo;
        }

        internal static bool LeaderBoardExists(Result dbResult)
        {
            return dbResult != null && dbResult.LeaderBoards != null && dbResult.LeaderBoards.Count > 0;
        }

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

        internal static IPagedList<Models.LeaderBoard> GetPagedLeaderBoards(ICollection<LeaderBoard> leaderBoard, int? lBoardPage)
        {
            var sortedlBoard = leaderBoard.OrderBy(s => s.YellowPoints);
            UpdateRankForCustomSortedLeaderBoards(ref sortedlBoard);

            return sortedlBoard.ToPagedList(lBoardPage ?? 1, pageSize);
        }

        private static void UpdateRankForCustomSortedLeaderBoards(ref IOrderedEnumerable<LeaderBoard> sortedlBoard)
        {
            // TODO: Fix rank if same as previous row.
            int rank = 0;
            foreach (var item in sortedlBoard)
            {
                item.Rank = ++rank;
            }
        }

        #region //private IPagedList<Models.LeaderBoard> GetSortedAndPagedLeaderBoards(int resultID, string sortBy)
        //{
        //    var lBoard =
        //        from l in db.LeaderBoards
        //        where l.ResultID == resultID
        //        select l;

        //    bool isSameField = LeaderBoardSortOrder != null && sortBy.Equals(GetSortField(LeaderBoardSortOrder));
        //    bool isAscending = !sortBy.Equals(LeaderBoardSortOrder);

        //    LeaderBoardSortOrder = sortBy ?? DefaultLBoardSortOrder;
        //    if (isSameField && isAscending)
        //        LeaderBoardSortOrder += " desc";

        //    IQueryable<Models.LeaderBoard> sortedlBoard = null;
        //    switch(GetSortField(LeaderBoardSortOrder))
        //    {
        //        case "Name":
        //        default:
        //            sortedlBoard = (isAscending) ? lBoard.OrderBy(s => s.Athlete.Name) : lBoard.OrderByDescending(s => s.Athlete.Name);
        //            break;
        //        case "Min":
        //            sortedlBoard = (isAscending) ? lBoard.OrderBy(s => s.ElapsedTimes.Min) : lBoard.OrderByDescending(s => s.ElapsedTimes.Min) ;
        //            break;
        //        case "Avg":
        //            sortedlBoard = (isAscending) ? lBoard.OrderBy(s => s.ElapsedTimes.Average) : lBoard.OrderByDescending(s => s.ElapsedTimes.Average);
        //            ModelUtils.UpdateRankForCustomSortedLeaderBoards(ref sortedlBoard);
        //            break;
        //        //...
        //    }

        //    //UpdateRankForCustomSortedLeaderBoards(ref sortedlBoard);

        //    return sortedlBoard.ToPagedList(lbPage ?? 1, pageSize);
        //}

        //private string GetSortField(string sortBy)
        //{
        //    int pos = sortBy.IndexOf(" ");
        //    if (pos > 0)
        //    {
        //        return sortBy.Substring(0, pos);
        //    }
        //    return sortBy;
        //}
        #endregion
    }
}