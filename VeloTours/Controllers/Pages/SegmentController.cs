using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeloTours.DAL.Segment;
using VeloTours.Models;
using VeloTours.ViewModel;

namespace VeloTours.Controllers.Pages
{
    public class SegmentController : Controller
    {
        private TourModelContainer db = new TourModelContainer();

        public ActionResult Segment(int segment, int athlete, int? area)
        {
            Segment segmentObj = db.Segments.Find(segment);
            ViewBag.Segment = segment;
            ViewBag.Athlete = athlete;
            ViewBag.Area = area;
            
            return View(segmentObj);
        }

        public ActionResult Area(int area, int? athlete)
        {
            SegmentArea segmentArea = db.SegmentAreas.Find(area);
            ViewBag.Area = area;
            ViewBag.Athlete = athlete;
            
            return View(segmentArea);
        }

        public ActionResult Index(int? athlete)
        {
            List<Country> countries = db.Countries.ToList();
            ViewBag.Athlete = athlete;

            return View(countries);
        }

        public ActionResult SegmentUpdate(int segment, int athlete, int? area)
        {
            DAL.Segment.Update segmentUpdate = new DAL.Segment.Update(segment);
            var s = segmentUpdate.UpdateSegmentInfo();

            return RedirectToAction("Segment", "Segment", new { athlete = athlete, segment = segment, area = area });
        }

        public ActionResult AreaUpdate(int area, int athlete, bool? recursive = false)
        {
            DAL.Area.Update areaUpdate = new DAL.Area.Update(area);
            areaUpdate.UpdateArea(false);

            return RedirectToAction("Area", "Segment", new { athlete = athlete, area = area });
        }
    }
}
