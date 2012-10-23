using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeloTours.DAL.Region;
using VeloTours.Models;
using VeloTours.ViewModels;

namespace VeloTours.Controllers.Pages
{
    public class RegionController : Controller
    {
        public ActionResult Index(int area, int? athlete, int? lbPage)
        {
            RegionViewModel areaViewModel = GetAreaViewModel(area, athlete ?? 0, lbPage);

            ViewBag.Area = area;
            ViewBag.Athlete = athlete;
            return View(areaViewModel);
        }

        public ActionResult Update(int area, int athlete, bool? effort = false)
        {
            RegionUpdate updater = new RegionUpdate(area);
            updater.Update((bool)effort);

            return RedirectToAction("Index", "Area", new { athlete = athlete, area = area });
        }

        private RegionViewModel GetAreaViewModel(int regionID, int athleteID, int? lbPage)
        {
            var db = new TourModelContainer();
            var dbRegion = db.Regions.Find(regionID);

            return new RegionViewModel(athleteID, dbRegion, lbPage ?? 1);
        }

    }
}
