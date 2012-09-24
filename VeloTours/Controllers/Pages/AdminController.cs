using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeloTours.Models;
using VeloTours.ViewModel;

namespace VeloTours.Controllers
{
    public class AdminController : Controller
    {
        private TourModelContainer db = new TourModelContainer();

        public ActionResult Index()
        {
            ViewBag.Message = "Strava segments made into Tours";
            return View();
        }
        
        public ActionResult ResetDB()
        {
            VeloTours.DAL.InitDB.CreateAndInitIfEmpty(db);
            return View("Index");
        }
    }
}
