using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeloTours.Models;
using VeloTours.ViewModels;

namespace VeloTours.Controllers
{
    public class HomeController : Controller
    {
        private TourModelContainer db = new TourModelContainer();
        
        public ActionResult Index(int? athlete)
        {
            ViewBag.Message = "Strava segments made into Tours";

            VeloTours.DAL.InitDB.CreateAndInitIfEmpty(db);  // TODO: Temporary

            Athlete athleteObj = db.Athletes.Find(athlete);
            return View(athleteObj);
        }

        [HttpPost]
        public ActionResult Index(int athlete)
        {
            try
            {
                if(athlete > 0)
                    return RedirectToAction("Index", "Segment", new { athlete = athlete });
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult About(int? athlete)
        {
            ViewBag.Message = "Your app description page.";
            ViewBag.Athlete = athlete;

            return View();
        }

        public ActionResult Contact(int? athlete)
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Athlete = athlete;

            return View();
        }

        public ActionResult ShowOriginalAuthor(int athlete)
        {
            ViewBag.Athlete = athlete;
            return RedirectToAction("Index", "Segment", new { Athlete = athlete });
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}
