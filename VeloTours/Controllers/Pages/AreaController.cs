using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            SegmentAreaViewModel areaViewModel = GetAreaViewModel(area, athlete ?? 0, lbPage);

            ViewBag.Area = area;
            ViewBag.Athlete = athlete;
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
