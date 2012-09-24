using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeloTours.Models;
using VeloTours.ViewModel;

namespace VeloTours.Controllers.Pages
{
    public class SegmentController : Controller
    {
        private TourModelContainer db = new TourModelContainer();

        public ActionResult Segment(int segment, int athlete)
        {
            Segment segmentObj = db.Segments.Find(segment);
            ViewBag.Segment = segment;
            ViewBag.Athlete = athlete;
            
            return View(segmentObj);
        }

        public ActionResult Area(int area, int athlete)
        {
            SegmentArea segmentArea = db.SegmentAreas.Find(area);
            ViewBag.Area = area;
            ViewBag.Athlete = athlete;
            
            return View(segmentArea);
        }

        public ActionResult Index(int athlete)
        {
            List<Country> countries = db.Countries.ToList();
            ViewBag.Athlete = athlete;

            return View(countries);
        }
    }
}
