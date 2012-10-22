using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeloTours.Models;
using VeloTours.ViewModels;

namespace VeloTours.Controllers
{
    public class ProfileController : Controller
    {
        private TourModelContainer db = new TourModelContainer();

        public ActionResult Index(int id)
        {
            ViewBag.Message = "My Profile";

            Athlete athlete = db.Athletes.Find(id);
            return View(athlete);
        }

        //
        // GET: /Profiler/Edit/5

        public ActionResult Update(int id)
        {
            Athlete athlete = db.Athletes.Find(id);
            return View(athlete);
        }

        //
        // POST: /Profiler/Update/5

        [HttpPost]
        public ActionResult Update(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Athlete athlete = db.Athletes.Find(id);

                return RedirectToAction("Settings");
            }
            catch
            {
                return View();
            }
        }
    }
}
