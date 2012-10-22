using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeloTours.DAL.Segment;
using VeloTours.Models;
using VeloTours.ViewModel;

namespace VeloTours.Controllers.Pages
{
    public class SegmentController : Controller
    {
        int? lbPage = null;

        public ActionResult Index(int segment, int? athlete, int? lbPage)
        {
            this.lbPage = lbPage;
            SegmentViewModel segmentViewModel = GetSegmentViewModel(segment, athlete ?? 0);

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
                Athlete = new AthleteRideInfo(),
                Segment = dbSegment, 
                SegmentAreas = dbSegment.SegmentAreas,

                YellowYersey = (dbResult != null) ? dbResult.YellowYerseyLB : null,
                GreenYersey = (dbResult != null) ? dbResult.GreenYerseyLB : null,
                PolkaDotYersey = (dbResult != null) ? dbResult.PolkaDotYerseyLB : null,
            };

            RideUtil.UpdateViewModel(athleteID, dbResult, viewModel, lbPage);
            return viewModel;
        }
    }
}
