using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VeloTours.Helpers
{
    public class HtmlVeloHelpers
    {
        public static String AthleteIconLink(int athleteId)
        {
            return String.Format("<a id='athlete{0}' data-athleteid='{0}' class='athleteLink' href='#' rel='popover'><i class='icon-user'></i></a>", athleteId);
        }

        public static String AthleteNameLink(string name, int athleteId)
        {
            return String.Format("<a id='athlete{0}' data-athleteid='{0}' class='athleteLink' href='#' rel='popover'>{1}</a>", athleteId, name);
        }

        public static MvcHtmlString AthleteNameLink(Models.LeaderBoard lboard)
        {
            if (lboard == null)
                return MvcHtmlString.Create(String.Empty);
            
            return AthleteNameLink(lboard.Athlete);
        }

        public static MvcHtmlString AthleteNameLink(Models.Athlete athlete)
        {
            return CreateAthletePopover(athlete, athlete.Name);
        }

        public static MvcHtmlString AthleteIconLink(Models.Athlete athlete)
        {
            return CreateAthletePopover(athlete, athlete.Name);
        }

        private static MvcHtmlString CreateAthletePopover(Models.Athlete athlete, string innerHtml)
        {
            //return String.Format("<a id='athlete{0}' data-athleteid='{0}' class='athleteLink' href='#' rel='popover'>{1}</a>", athleteId, name);
            TagBuilder tb = new TagBuilder("a");
            tb.InnerHtml = innerHtml;
            //tb.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tb.MergeAttribute("id", "athlete" + athlete.AthleteID);
            tb.MergeAttribute("data-athleteid", athlete.AthleteID.ToString());
            tb.MergeAttribute("class", "athleteLink");
            tb.MergeAttribute("href", "#");
            tb.MergeAttribute("rel", "popover");
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.Normal));
        }
    }
}