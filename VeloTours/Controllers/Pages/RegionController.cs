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
        public ActionResult Index(int region, int? athlete, int? lbPage)
        {
            RegionViewModel regionViewModel = GetRegionViewModel(region, athlete ?? 0, lbPage);

            ViewBag.Athlete = athlete;
            ViewBag.Region = region;
            ViewBag.RideListType = "area";
            ViewBag.HasLeaderBoard = (regionViewModel.Region.Result != null && regionViewModel.LeaderBoard != null && regionViewModel.LeaderBoard.Count > 0);
            return View(regionViewModel);
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Update(int region, int athlete)
        {
            RegionUpdate regionUpdater = new RegionUpdate(region);
            regionUpdater.Update();

            return RedirectToAction("Index", "Region", new { athlete = athlete, region = region });
        }

        private RegionViewModel GetRegionViewModel(int regionID, int athleteID, int? lbPage)
        {
            var db = new TourModelContainer();
            var dbRegion = db.Regions.Find(regionID);

            return new RegionViewModel(athleteID, dbRegion, lbPage ?? 1);
        }

    }
}
