﻿using System;
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
            SegmentViewModel segmentModel = GetSegmentViewModel(segment, athlete);

            ViewBag.Segment = segment;
            ViewBag.Athlete = athlete;
            ViewBag.Area = area;
            return View(segmentModel);
        }

        public ActionResult Update(int segment, int athlete, int? area, bool? effort = false)
        {
            SegmentUpdate segmentUpdate = new SegmentUpdate(segment);
            segmentUpdate.UpdateSegment();
            if ((bool)effort)
                segmentUpdate.UpdateEfforts(segment);

            return RedirectToAction("Index", new { athlete = athlete, segment = segment, area = area });
        }

        private SegmentViewModel GetSegmentViewModel(int segmentID, int athlete)
        {
            Segment dbSegment = db.Segments.Find(segmentID);
            var viewModel = new SegmentViewModel { Segment = dbSegment, Athlete = new AthleteRideInfo(), };
            viewModel.Athlete.ElapsedTimes = new ElapsedTimes(); //TODO;

            // Update from other
            var dbResult = dbSegment.Result;
            if (dbResult != null)
            {
                Models.LeaderBoard lBoardsKOM = dbResult.LeaderBoards.First();
                viewModel.KomSpeed = (viewModel.Segment.Info.Distance / lBoardsKOM.ElapsedTimes.Min) * 3.6;
                
                if(athlete > 0)
                {
                    var lBoardAthlete =
                        from l in db.LeaderBoards
                        where l.AthleteID == athlete 
                           && l.ResultID == dbResult.ResultID
                        select l;

                    if(lBoardAthlete.Count() == 1)
                    {
                        Models.LeaderBoard lAthlete = lBoardAthlete.First();
                        viewModel.Athlete.BehindKom = lAthlete.ElapsedTimes.Min - lBoardsKOM.ElapsedTimes.Min;
                        viewModel.Athlete.BehindKomPercentage = ((double)lAthlete.ElapsedTimes.Min / (double)lBoardsKOM.ElapsedTimes.Min - 1) * 100;
                        viewModel.Athlete.ElapsedTimes = lAthlete.ElapsedTimes;
                        viewModel.Athlete.NoRidden = lAthlete.NoRidden;
                        viewModel.Athlete.Position = lAthlete.Rank;
                        viewModel.Athlete.PositionPercentage = (double)lAthlete.Rank / (double)dbResult.LeaderBoards.Count() * 100;
                        //viewModel.UsersTimePrevious =
                        //viewModel.UsersChangePos =
                    }
                }
            }
            return viewModel;
        }
    }
}
