using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeloTours.Models;
using VeloTours.ViewModel;

namespace VeloTours.Controllers
{
    public class HomeController : Controller
    {
        private TourContext db = new TourContext();

        public ActionResult Index()
        {
            ViewBag.Message = "Strava segments made into Tours";
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your app description page.";

            var data = from athlete in db.Athletes
                       select new AthleteInfo()
                       {
                           AthleteCount = athlete.AthleteID
                       };

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
