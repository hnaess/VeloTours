using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeloTours.DAL.Area;
using VeloTours.DAL.Segment;
using VeloTours.Models;
using VeloTours.ViewModel;

namespace VeloTours.Controllers.Pages
{
    public class SegmentController : Controller
    {
        private TourModelContainer db = new TourModelContainer();

        public ActionResult Index(int segment, int athlete, int? area)
        {
            //Segment segmentModel = db.Segments.Find(segment);
            SegmentViewModel segmentModel = GetSegmentViewModel(segment);

            ViewBag.Segment = segment;
            ViewBag.Athlete = athlete;
            ViewBag.Area = area;
            return View(segmentModel);
        }

        public ActionResult Update(int segment, int athlete, int? area, bool? effort = false)
        {
            SegmentUpdate segmentUpdate = new SegmentUpdate(segment);
            segmentUpdate.UpdateSegment((bool)effort);

            return RedirectToAction("Index", new { athlete = athlete, segment = segment, area = area });
        }

        private SegmentViewModel GetSegmentViewModel(int segmentID)
        {
            Segment model = db.Segments.Find(segmentID);
            return new SegmentViewModel
            {
                AvgGrade = model.AvgGrade,
                ClimbCategory = model.ClimbCategory,
                Description = model.Description,
                Distance = model.Distance,
                ElevationGain = model.ElevationGain,
                ElevationHigh = model.ElevationHigh,
                ElevationLow = model.ElevationLow,
                GradeType = model.GradeType,
                LastUpdated = model.LastUpdated,
                Name = model.Name,
                NoRidden = model.NoRidden,
                NoRiders = model.NoRiders,
                PictureUri = model.PictureUri,
                SegmentID = model.SegmentID,

                // + new fields?
                //KomSpeed =
                //BehindKom =
                //BehindKomPercentage =
                //UsersPosition =
                //UsersPositionPercentage =
                //UsersTime =
                //UsersTimePrevious =
                //UsersChangePos =
            };
        }
    }
}
