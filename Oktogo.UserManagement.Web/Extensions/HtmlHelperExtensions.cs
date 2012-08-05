using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;

using Oktogo.UserManagement.Web.ViewModels;

namespace Oktogo.UserManagement.Web.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString ActionImage(this HtmlHelper html, string imagePath, string alt, string action, object routeValues)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            // build the <img> tag
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            imgBuilder.MergeAttribute("alt", alt);
            string imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", url.Action(action, routeValues));
            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            string anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }

        public static MvcHtmlString PageLinks(this HtmlHelper html, PagerData pagerData, Func<int, string> pageUrl)
        {
            var result = new StringBuilder();
            for (int i = 1; i <= pagerData.PagesCount; i++)
            {
                var tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString(CultureInfo.InvariantCulture);
                if (i == pagerData.PageNumber)
                {
                    tag.AddCssClass("selected");
                }
                result.Append(tag);
            }

            return MvcHtmlString.Create(result.ToString());
        }
    }
}