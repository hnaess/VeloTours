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
            ViewBag.Athlete = athlete;

            return View(countries);
        }

        public ActionResult UpdateRegion(int athlete, int region, bool? effort = false)
        {
            RegionUpdate regionUpdate = new RegionUpdate(region);
            regionUpdate.Update((bool)effort);

            return RedirectToAction("Index", new { athlete = athlete, region = region });
        }

        public ActionResult Update(int athlete, int country, bool? effort = false)
        {
            CountryUpdate regionUpdate = new CountryUpdate(country);
            regionUpdate.Update((bool)effort);

            return RedirectToAction("Index", new { athlete = athlete, country = country });
        }
    }
}
