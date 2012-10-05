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
            //Models.SegmentArea segmentArea = db.SegmentAreas.Find(area);

            TourModelContainer db = new TourModelContainer();
            Models.SegmentArea segmentArea = db.SegmentAreas.Find(area);

            SegmentAreaViewModel viewModel = GetSegmentArea(segmentArea);
            foreach (Models.Segment segment in segmentArea.Segments)
            {
                GetSegment(viewModel, segment);
            }

            ViewBag.Area = area;
            ViewBag.Athlete = athlete;
            return View(viewModel);
        }

        public ActionResult Update(int area, int athlete, bool? effort = false)
        {
            AreaUpdate areaUpdate = new AreaUpdate(area);
            areaUpdate.UpdateArea((bool)effort);

            return RedirectToAction("Area", "Segment", new { athlete = athlete, area = area });
        }


        private static void GetSegment(SegmentAreaViewModel viewModel, Models.Segment segment)
        {
            viewModel.Segments.Add(new SegmentViewModel
            {
                AvgGrade = segment.AvgGrade,
                ClimbCategory = segment.ClimbCategory,
                Description = segment.Description,
                Distance = segment.Distance,
                ElevationGain = segment.ElevationGain,
                ElevationHigh = segment.ElevationHigh,
                ElevationLow = segment.ElevationLow,
                GradeType = segment.GradeType,
                LastUpdated = segment.LastUpdated,
                Name = segment.Name,
                NoRidden = segment.NoRidden,
                NoRiders = segment.NoRiders,
                PictureUri = segment.PictureUri,
                SegmentID = segment.SegmentID,

                // + new fields?
                //KomSpeed =
                //BehindKom =
                //BehindKomPercentage =
                //UsersPosition =
                //UsersPositionPercentage =
                //UsersTime =
                //UsersTimePrevious =
                //UsersChangePos =
            });
        }

        private static SegmentAreaViewModel GetSegmentArea(Models.SegmentArea segmentArea)
        {
            SegmentAreaViewModel viewModel = new SegmentAreaViewModel()
            {
                AvgGrade = segmentArea.AvgGrade,
                Description = segmentArea.Description,
                Distance = segmentArea.Distance,
                ElevationGain = segmentArea.ElevationGain,
                LastUpdated = segmentArea.LastUpdated,
                PictureUri = segmentArea.PictureUri,
                SegmentAreaID = segmentArea.SegmentAreaID,

                //
                //NoRidden = segment.NoRidden,
                //KomSpeed =
                //BehindKom =
                //BehindKomPercentage =
                //UsersPosition =
                //UsersPositionPercentage =
                //UsersTime =
                //UsersTimePrevious =
                //UsersChangePos =

                // example of a new
                Position = 1234,
            };
            return viewModel;
        }

    }
}
