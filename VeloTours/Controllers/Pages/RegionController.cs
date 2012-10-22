using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeloTours.DAL.Area;
using VeloTours.Models;
using VeloTours.ViewModel;

namespace VeloTours.Controllers.Pages
{
    public class AreaController : Controller
    {
        int? lbPage = null;

        public ActionResult Index(int area, int? athlete, int? lbPage)
        {
            this.lbPage = lbPage;
            SegmentAreaViewModel areaViewModel = GetAreaViewModel(area, athlete ?? 0);

            ViewBag.Area = area;
            ViewBag.Athlete = athlete;
            return View(areaViewModel);
        }

        public ActionResult Update(int area, int athlete, bool? effort = false)
        {
            AreaUpdate areaUpdate = new AreaUpdate(area);
            areaUpdate.UpdateArea((bool)effort);

            return RedirectToAction("Index", "Area", new { athlete = athlete, area = area });
        }

        private SegmentAreaViewModel GetAreaViewModel(int areaID, int athleteID)
        {
            var db = new TourModelContainer();
            var dbArea = db.SegmentAreas.Find(areaID);
            var dbResult = dbArea.Result;

            SegmentAreaViewModel viewModel = new SegmentAreaViewModel()
            {
                Athlete = new AthleteRideInfo(),
                SegmentArea = dbArea,
                Segments = new List<SegmentViewModel>(),

                YellowYersey = (dbResult != null) ? dbResult.YellowYerseyLB : null,
                GreenYersey = (dbResult != null) ? dbResult.GreenYerseyLB : null,
                PolkaDotYersey = (dbResult != null) ? dbResult.PolkaDotYerseyLB : null,
            };
            
            foreach (Models.Segment segment in dbArea.Segments)
            {
                var segmentViewModel = new SegmentViewModel() { Segment = segment, };
                viewModel.Segments.Add(segmentViewModel);

                var dbResultSegment = segment.Result;
                RideUtil.UpdateViewModel(athleteID, dbResultSegment, segmentViewModel, lbPage);
            }

            RideUtil.UpdateViewModel(athleteID, dbResult, viewModel, lbPage);

            return viewModel;
        }
    }
}
