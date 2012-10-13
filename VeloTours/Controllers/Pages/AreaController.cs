using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeloTours.DAL.Area;
using VeloTours.Models;
using VeloTours.ViewModel;
namespace VeloTours.Controllers.Pages
{
    public class AreaController : Controller
    {
        public ActionResult Index(int area, int? athlete)
        {
            TourModelContainer db = new TourModelContainer();
            Models.SegmentArea dbArea = db.SegmentAreas.Find(area);

            SegmentAreaViewModel viewModel = new SegmentAreaViewModel() { 
                SegmentArea = dbArea, 
                KomSpeed = 0, // TODO
                Athlete = new AthleteRideInfo(), // TODO
            };
            viewModel.Athlete.ElapsedTimes = new ElapsedTimes(); //TODO;
            viewModel.Segments = new List<SegmentViewModel>();
            foreach (Models.Segment segment in dbArea.Segments)
            {
                viewModel.Segments.Add(new SegmentViewModel { Segment = segment });
            }

            ViewBag.Area = area;
            ViewBag.Athlete = athlete;
            return View(viewModel);
        }

        public ActionResult Update(int area, int athlete, bool? effort = false)
        {
            AreaUpdate areaUpdate = new AreaUpdate(area);
            areaUpdate.UpdateArea((bool)effort);

            return RedirectToAction("Index", "Area", new { athlete = athlete, area = area });
        }
    }
}
