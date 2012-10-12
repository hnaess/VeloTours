using System;
using System.Web;

namespace VeloTours.Helpers
{
    public class HtmlFormatters
    {
        public static String AthleteIconLink(int athleteId)
        {
            return String.Format("<a id='athlete{0}' data-athleteid='{0}' class='athleteLink' href='#' rel='popover'><i class='icon-user'></i></a>", athleteId);
        }

        public static String AthleteNameLink(string name, int athleteId)
        {
            return String.Format("<a id='athlete{0}' data-athleteid='{0}' class='athleteLink' href='#' rel='popover'>{1}</a>", athleteId, name);
        }
    }
}