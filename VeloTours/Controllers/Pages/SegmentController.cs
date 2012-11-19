﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeloTours.DAL.Segment;
using VeloTours.Models;
using VeloTours.ViewModels;

namespace VeloTours.Controllers.Pages
{
    public class SegmentController : Controller
    {
        public ActionResult Index(int segment, int? athlete, int? lbPage)
        {
            SegmentViewModel viewModel = GetSegmentViewModel(segment, athlete ?? 0, lbPage);

            ViewBag.Segment = segment;
            ViewBag.Athlete = athlete ?? 0;
            ViewBag.HasLeaderBoard = (viewModel.Segment.Result != null && viewModel.LeaderBoard != null && viewModel.LeaderBoard.Count > 0);
            return View(viewModel);
        }

        public ActionResult Update(int segment, int athlete)
        {
            SegmentUpdate segmentUpdate = new SegmentUpdate(segment);
            segmentUpdate.UpdateSegment();
            segmentUpdate.UpdateEfforts(segment);

            return RedirectToAction("Index", new { athlete = athlete, segment = segment });
        }

        private SegmentViewModel GetSegmentViewModel(int segmentID, int athleteID, int? lbPage) //, string sortBy)
        {
            var db = new TourModelContainer();
            var dbSegment = db.Segments.Find(segmentID);

            return new SegmentViewModel(athleteID, dbSegment, lbPage ?? 1);
        }
    }
}
