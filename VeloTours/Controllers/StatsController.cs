using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VeloTours.Controllers
{
    public class StatsController : Controller
    {
        //
        // GET: /Stats/

        public ActionResult Segments(string athleteId, int segmentsId)
        {
            ViewBag.AthledeId = athleteId;
            ViewBag.SegmentsId = segmentsId;
            return View();
        }
    }
}
