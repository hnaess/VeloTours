using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using VeloTours.Models;

namespace VeloTours.Helpers
{
    public static class HtmlHelpers
    {
        public static string SimpleLink(this HtmlHelper html, string url, string text)
        {
            return String.Format("<a href=\"{0}\">{1}</a>", url, text);
        }

        public static MvcHtmlString HtmlLink(this HtmlHelper html, string url, string text, object htmlAttributes)
        {
            TagBuilder tb = new TagBuilder("a");
            tb.InnerHtml = text;
            tb.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tb.MergeAttribute("href", url);
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string url, string altText, object htmlAttributes)
        {
            TagBuilder builder = new TagBuilder("img");
            builder.Attributes.Add("src", url);
            builder.Attributes.Add("alt", altText);
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static String Truncate(this HtmlHelper helper, string input, int length)
        {
            if (input.Length <= length)
            {
                return input;
            }
            else
            {
                return input.Substring(0, length) + "...";
            }
        }
    }
}