using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeloTours.DAL.Country;
using VeloTours.DAL.Region;
using VeloTours.Models;

namespace VeloTours.Controllers.Pages
{
    public class CountryController : Controller
    {
        private TourModelContainer db = new TourModelContainer();

        public ActionResult Index(int? athlete)
        {
            List<Models.Country> countries = db.Countries.ToList();
            ViewBag.Athlete = athlete ?? 0;

            return View(countries);
        }

        public ActionResult UpdateRegion(int athlete, int region)
        {
            RegionUpdate regionUpdate = new RegionUpdate(region);
            regionUpdate.Update();

            return RedirectToAction("Index", new { athlete = athlete, region = region });
        }

        public ActionResult Update(int athlete, int country)
        {
            CountryUpdate regionUpdate = new CountryUpdate(country);
            regionUpdate.Update();

            return RedirectToAction("Index", new { athlete = athlete, country = country });
        }
    }
}
