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

        public String Index(int id)
        {
            return "TODO" + id;
        }

        public ActionResult Segment(int id)
        {
            Segment segment = db.Segments.Find(id);
            return View(segment);
        }

        public ActionResult Area(int id)
        {
            SegmentArea segmentArea = db.SegmentAreas.Find(id);
            return View(segmentArea);
        }

        // <iframe allowtransparency="true" frameborder="0" height="405" scrolling="no" src="http://app.strava.com/segments/632847/embed" width="590"></iframe>
    }
}
