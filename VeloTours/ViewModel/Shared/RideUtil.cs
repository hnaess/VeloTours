using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using PagedList;
using VeloTours.Models;

namespace VeloTours.ViewModel
{
    public class RideUtil
    {
        //private string DefaultLBoardSortOrder = "Min";
        private static int pageSize = 20;
        //string lbSortOrder = null;

        internal static IPagedList<LeaderBoard> CreateBlankPagedList()
        {
            return new List<Models.LeaderBoard>().ToPagedList(1, 1);
        }

        internal static void UpdateViewModel(TourModelContainer db, int athleteID, Result dbResult, int? lbPage, SegmentViewModel viewModel)
        {
            if (LeaderBoardExists(dbResult))
            {
                var lBoardsKOM = dbResult.LeaderBoards.First();
                viewModel.KomSpeed = viewModel.Segment.Info.SpeedInKmH(lBoardsKOM);
                viewModel.LeaderBoard = GetPagedLeaderBoards(db, dbResult.ResultID, lbPage);

                if (athleteID > 0)
                {
                    Models.LeaderBoard lBoard = AddAthleteToViewModel(db, athleteID, viewModel.Athlete, dbResult, lBoardsKOM);
                    viewModel.ImprovementHint = CreateImprovementHint(viewModel.Athlete.ElapsedTimes.Min, dbResult.LeaderBoards, lBoard);
                }
            }
            else
            {
                viewModel.LeaderBoard = CreateBlankPagedList();
            }
        }

        internal static void UpdateViewModel(TourModelContainer db, int athleteID, Result dbResult, int? lbPage, SegmentAreaViewModel viewModel)
        {
            if (LeaderBoardExists(dbResult))
            {
                var lBoardsKOM = dbResult.LeaderBoards.First();
                viewModel.KomSpeed = viewModel.SegmentArea.Info.SpeedInKmH(lBoardsKOM);
                viewModel.LeaderBoard = GetPagedLeaderBoards(db, dbResult.ResultID, lbPage);

                if (athleteID > 0)
                {
                    Models.LeaderBoard lBoard = AddAthleteToViewModel(db, athleteID, viewModel.Athlete, dbResult, lBoardsKOM);
                }
            }
            else
            {
                viewModel.LeaderBoard = CreateBlankPagedList();
            }
        }

        internal static Models.LeaderBoard AddAthleteToViewModel(TourModelContainer db, int athlete, AthleteRideInfo athleteRideInfo, Result dbResult, Models.LeaderBoard lBoardKOM)
        {
            var lBoardAthleteQuery =
                from l in db.LeaderBoards
                where l.AthleteID == athlete
                   && l.ResultID == dbResult.ResultID
                select l;

            if (lBoardAthleteQuery.Count() == 1)
            {
                var lBoardAthlete = lBoardAthleteQuery.First();
                athleteRideInfo.BehindKom = lBoardAthlete.ElapsedTimes.Min - lBoardKOM.ElapsedTimes.Min;
                athleteRideInfo.BehindKomPercentage = ((double)lBoardAthlete.ElapsedTimes.Min / (double)lBoardKOM.ElapsedTimes.Min - 1) * 100;
                athleteRideInfo.ElapsedTimes = lBoardAthlete.ElapsedTimes;
                athleteRideInfo.NoRidden = lBoardAthlete.NoRidden;
                athleteRideInfo.Position = lBoardAthlete.Rank;
                athleteRideInfo.PositionPercentage = (double)lBoardAthlete.Rank / (double)dbResult.LeaderBoards.Count() * 100;
                return lBoardAthlete;
            }
            else if (lBoardAthleteQuery.Count() > 1)
            {
                Debug.Fail("What?");
            }
            return null;
        }

        internal static bool LeaderBoardExists(Result dbResult)
        {
            return dbResult != null && dbResult.LeaderBoards != null && dbResult.LeaderBoards.Count > 0;
        }

        private static string CreateImprovementHint(int duration, ICollection<LeaderBoard> lBoard, Models.LeaderBoard lAthlete)
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

        internal static IPagedList<Models.LeaderBoard> GetPagedLeaderBoards(TourModelContainer db, int resultID, int? lBoardPage)
        {
            var lBoard =
                from l in db.LeaderBoards
                where l.ResultID == resultID
                select l;

            //IQueryable<Models.LeaderBoard> sortedlBoard = null;
            var sortedlBoard = lBoard.OrderBy(s => s.YellowPoints);
            //UpdateRankForCustomSortedLeaderBoards(ref sortedlBoard);

            return sortedlBoard.ToPagedList(lBoardPage ?? 1, pageSize);
        }

        //private IPagedList<Models.LeaderBoard> GetSortedAndPagedLeaderBoards(int resultID, string sortBy)
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
        
        internal static void UpdateRankForCustomSortedLeaderBoards(ref IQueryable<Models.LeaderBoard> lBoard)
        {
            int rank = 0;
            foreach (var item in lBoard)
            {
                item.Rank = ++rank;
            }
        }

        internal static void UpdateResultWithYersey(Result dbResult)
        {
            var lBoard =
                (from l in dbResult.LeaderBoards
                where l.ResultID == dbResult.ResultID
                select l).Take(1);


        }
    }
}