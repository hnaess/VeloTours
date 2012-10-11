using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeloTours.DAL.Area;
using VeloTours.DAL.Segment;
using VeloTours.Models;
using VeloTours.ViewModel;
using PagedList;
using System.Diagnostics;

namespace VeloTours.Controllers.Pages
{
    public class SegmentController : Controller
    {
        string DefaultLBoardSortOrder = "Min";

        int? lbPage = null;
        string lbSortOrder = null;
        int pageSize = 20;

        private TourModelContainer db = new TourModelContainer();

        private string LeaderBoardSortOrder
        {
            get { return ViewBag.SortOrder; }
            set { ViewBag.SortOrder = value; }
        }

        public ActionResult Index(int segment, int athlete, int? area, int? lbPage, string lbSortBy)
        {
            this.lbPage = lbPage;
            SegmentViewModel segmentModel = GetSegmentViewModel(segment, athlete, lbSortBy ?? DefaultLBoardSortOrder);

            ViewBag.Segment = segment;
            ViewBag.Athlete = athlete;
            ViewBag.Area = area;
            return View(segmentModel);
        }

        public ActionResult Update(int segment, int athlete, int? area, bool? effort = false)
        {
            SegmentUpdate segmentUpdate = new SegmentUpdate(segment);
            segmentUpdate.UpdateSegment();
            if ((bool)effort)
                segmentUpdate.UpdateEfforts(segment);

            return RedirectToAction("Index", new { athlete = athlete, segment = segment, area = area });
        }

        private SegmentViewModel GetSegmentViewModel(int segmentID, int athlete, string sortBy)
        {
            Segment dbSegment = db.Segments.Find(segmentID);
            var viewModel = new SegmentViewModel { Segment = dbSegment, Athlete = new AthleteRideInfo(), };
            viewModel.Athlete.ElapsedTimes = new ElapsedTimes(); //TODO;

            // Update from other
            var dbResult = dbSegment.Result;
            bool leaderBoardExists = dbResult != null && dbResult.LeaderBoards != null && dbResult.LeaderBoards.Count > 0;
            if (leaderBoardExists)
            {
                //http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
                //https://github.com/TroyGoode/PagedList

                Models.LeaderBoard lBoardsKOM = dbResult.LeaderBoards.First();
                viewModel.KomSpeed = (viewModel.Segment.Info.Distance / lBoardsKOM.ElapsedTimes.Min) * 3.6;
                //viewModel.LeaderBoard = viewModel.Segment.Result.LeaderBoards.ToPagedList(lbPage ?? 1, pageSize);
                viewModel.LeaderBoard = GetSortedLeaderBoards(dbResult.ResultID, sortBy); //.ToPagedList(lbPage ?? 1, pageSize);

                if (athlete > 0)
                {
                    var lBoardAthlete =
                        from l in db.LeaderBoards
                        where l.AthleteID == athlete
                           && l.ResultID == dbResult.ResultID
                        select l;

                    if (lBoardAthlete.Count() == 1)
                    {
                        UpdateViewModelWithAthleteInfo(viewModel, dbResult.LeaderBoards.Count(), lBoardsKOM, lBoardAthlete.First());
                        viewModel.ImprovementHint = CreateImprovementHint(viewModel.Athlete.ElapsedTimes.Min, dbResult.LeaderBoards, lBoardAthlete.First());
                    }
                    else
                    {
                        Debug.Fail("What?");
                    }
                }
            }
            else
            {
                viewModel.LeaderBoard = new List<Models.LeaderBoard>().ToPagedList(1, 1);
            }
            return viewModel;
        }

        private static void UpdateViewModelWithAthleteInfo(SegmentViewModel viewModel, int riders, Models.LeaderBoard lBoardsKOM, Models.LeaderBoard lAthlete)
        {
            viewModel.Athlete.BehindKom = lAthlete.ElapsedTimes.Min - lBoardsKOM.ElapsedTimes.Min;
            viewModel.Athlete.BehindKomPercentage = ((double)lAthlete.ElapsedTimes.Min / (double)lBoardsKOM.ElapsedTimes.Min - 1) * 100;
            viewModel.Athlete.ElapsedTimes = lAthlete.ElapsedTimes;
            viewModel.Athlete.NoRidden = lAthlete.NoRidden;
            viewModel.Athlete.Position = lAthlete.Rank;
            viewModel.Athlete.PositionPercentage = (double)lAthlete.Rank / (double)riders * 100;
            //viewModel.UsersTimePrevious =
            //viewModel.UsersChangePos =
        }

        private static string CreateImprovementHint(int duration, ICollection<LeaderBoard> lBoard, Models.LeaderBoard lAthlete)
        {
            int improveA = duration < 300 ? 3 : 10;
            int improveB = duration < 300 ? 10 : 30;
            int rankImproveA = (from lb in lBoard where lb.ElapsedTimes.Min <= (duration - improveA) select lb.Rank).Last();
            int rankImproveB = (from lb in lBoard where lb.ElapsedTimes.Min <= (duration - improveB) select lb.Rank).Last();

            string s = String.Format("Ride {0} seconds faster and improve {1} positions, or {2} seconds faster and improve {3}",
                improveA, (lAthlete.Rank - rankImproveA - 1), improveB, (lAthlete.Rank - rankImproveB - 1));
            return s;
        }

        private IPagedList<Models.LeaderBoard> GetSortedLeaderBoards(int resultID, string sortBy)
        {
            var lBoard =
                from l in db.LeaderBoards
                where l.ResultID == resultID
                select l;

            bool isSameField = LeaderBoardSortOrder != null && sortBy.Equals(GetSortField(LeaderBoardSortOrder));
            bool isAscending = !sortBy.Equals(LeaderBoardSortOrder);

            LeaderBoardSortOrder = sortBy ?? DefaultLBoardSortOrder;
            if (isSameField && isAscending)
                LeaderBoardSortOrder += " desc";

            IQueryable<Models.LeaderBoard> sortedlBoard = null;
            switch(GetSortField(LeaderBoardSortOrder))
            {
                case "Name":
                default:
                    sortedlBoard = (isAscending) ? lBoard.OrderBy(s => s.Athlete.Name) : lBoard.OrderByDescending(s => s.Athlete.Name);
                    break;
                case "Min":
                    sortedlBoard = (isAscending) ? lBoard.OrderBy(s => s.ElapsedTimes.Min) : lBoard.OrderByDescending(s => s.ElapsedTimes.Min) ;
                    break;
                case "Avg":
                    sortedlBoard = (isAscending) ? lBoard.OrderBy(s => s.ElapsedTimes.Average) : lBoard.OrderByDescending(s => s.ElapsedTimes.Average);
                    UpdateRankForCustomSortedLeaderBoards(ref sortedlBoard);
                    break;
                //...
            }

            //UpdateRankForCustomSortedLeaderBoards(ref sortedlBoard);

            return sortedlBoard.ToPagedList(lbPage ?? 1, pageSize);
        }

        private void UpdateRankForCustomSortedLeaderBoards(ref IQueryable<Models.LeaderBoard> lBoard)
        {
            //TODO
            //throw new NotImplementedException();
        }

        private string GetSortField(string sortBy)
        {
            int pos = sortBy.IndexOf(" ");
            if (pos > 0)
            {
                return sortBy.Substring(0, pos);
            }
            return sortBy;
        }
    }
}
