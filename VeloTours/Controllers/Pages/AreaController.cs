using System;
using System.Web.Mvc;
using VeloTours.DAL.Area;
using VeloTours.Models;
using VeloTours.ViewModels;

namespace VeloTours.Controllers.Pages
{
    public class AreaController : Controller
    {
        public ActionResult Index(int area, int? athlete, int? lbPage)
        {
            int athleteID = athlete ?? 0;
            SegmentAreaViewModel areaViewModel = GetAreaViewModel(area, athleteID, lbPage);

            ViewBag.Athlete = athleteID;
            ViewBag.Area = area;
            ViewBag.RideID = area;
            ViewBag.RideListType = "segment";
            ViewBag.HasLeaderBoard = (areaViewModel.SegmentArea.Result != null && areaViewModel.LeaderBoard != null && areaViewModel.LeaderBoard.Count > 0);
            
            return View(areaViewModel);
        }

        public ActionResult Update(int area, int athlete)
        {
            AreaUpdate areaUpdate = new AreaUpdate(area);
            areaUpdate.Update();

            return RedirectToAction("Index", "Area", new { athlete = athlete, area = area });
        }

        private SegmentAreaViewModel GetAreaViewModel(int areaID, int athleteID, int? lbPage)
        {
            var db = new TourModelContainer();
            var dbArea = db.SegmentAreas.Find(areaID);

            return new SegmentAreaViewModel(athleteID, dbArea, lbPage ?? 1);
        }
    }
}
