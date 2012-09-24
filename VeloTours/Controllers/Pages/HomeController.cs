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
        private TourModelContainer db = new TourModelContainer();
        
        public ActionResult Index()
        {
            ViewBag.Message = "Strava segments made into Tours";

            VeloTours.DAL.InitDB.CreateAndInitIfEmpty(db);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
