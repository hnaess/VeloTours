using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using VeloTours.DAL.Area;
using VeloTours.DAL.Segment;
using VeloTours.Models;
using VeloTours.ViewModel;
using VeloTours.Controllers.Pages.Segment;

namespace VeloTours.Controllers.Pages
{
    public class SegmentController : Controller
    {
        int? lbPage = null;

        public ActionResult Index(int segment, int athlete, int? lbPage, string lbSortBy)
        {
            this.lbPage = lbPage;
            SegmentViewModel segmentViewModel = GetSegmentViewModel(segment, athlete); //, lbSortBy ?? DefaultLBoardSortOrder);

            ViewBag.Segment = segment;
            ViewBag.Athlete = athlete;
            return View(segmentViewModel);
        }

        public ActionResult Update(int segment, int athlete, bool? effort = false)
        {
            SegmentUpdate segmentUpdate = new SegmentUpdate(segment);
            segmentUpdate.UpdateSegment();
            if ((bool)effort)
                segmentUpdate.UpdateEfforts(segment);

            return RedirectToAction("Index", new { athlete = athlete, segment = segment });
        }

        private SegmentViewModel GetSegmentViewModel(int segmentID, int athleteID) //, string sortBy)
        {
            var db = new TourModelContainer();
            var dbSegment = db.Segments.Find(segmentID);
            var dbResult = dbSegment.Result;
            
            var viewModel = new SegmentViewModel
            {
                Athlete = new AthleteRideInfo() { ElapsedTimes = new ElapsedTimes() },       //TODO: Can we drop this new ElapsedTimes?
                Segment = dbSegment, 
                SegmentAreas = dbSegment.SegmentAreas,

                YellowYersey = (dbResult != null) ? dbResult.YellowYerseyLB : null,
                GreenYersey = (dbResult != null) ? dbResult.GreenYerseyLB : null,
                PolkaDotYersey = (dbResult != null) ? dbResult.PolkaDotYerseyLB : null,
            };

            ModelUtils.UpdateViewModel(db, athleteID, dbResult, lbPage, viewModel);
            return viewModel;
        }
    }
}
