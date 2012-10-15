using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VeloTours.Helpers
{
    public class HtmlVeloHelpers
    {
        public static MvcHtmlString AtheleteNameLink(Models.LeaderBoard lboard)
        {
            if (lboard == null)
                return MvcHtmlString.Create(String.Empty);
            
            return AtheleteNameLink(lboard.Athlete);
        }

        public static MvcHtmlString AtheleteNameLink(Models.Athlete athlete)
        {
            //return String.Format("<a id='athlete{0}' data-athleteid='{0}' class='athleteLink' href='#' rel='popover'>{1}</a>", athleteId, name);
            TagBuilder tb = new TagBuilder("a");
            tb.InnerHtml = athlete.Name;
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