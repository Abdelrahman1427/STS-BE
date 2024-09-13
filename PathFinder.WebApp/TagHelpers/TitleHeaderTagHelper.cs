using Microsoft.AspNetCore.Razor.TagHelpers;
using PathFinder.WebApp.Models;
using System.Text;

namespace PathFinder.WebApp.TagHelpers
{
    public class TitleHeaderTagHelper : TagHelper
    {
        public string Title { get; set; }
        public List<BreadCrumb> BreadCrumbs { get; set; } = null;
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.AppendFormat("<div class=\"page-header\">");
            output.Content.AppendFormat("<div class=\"row align-items-center\">");
            output.Content.AppendFormat("<div class=\"col\">");
            output.Content.AppendFormat($"<h3 class=\"page-title\">{Title}</h3>");
            if(BreadCrumbs != null)
            {
                output.Content.AppendFormat("<ul class=\"breadcrumb\">");
                foreach (var item in BreadCrumbs)
                {
                    if (item.URL != null)
                        output.Content.AppendFormat($"<li class=\"breadcrumb-item\"><a href=\"{item.URL}\">{item.Name}</a></li>");
                    else
                        output.Content.AppendFormat($"<li class=\"breadcrumb-item\">{item.Name}</li>");
                }
                output.Content.AppendFormat($"<li class=\"breadcrumb-item active\">{Title}</li>");
                output.Content.AppendFormat("</ul>");
            }
            output.Content.AppendFormat("</div> </div> </div>");
        }
    }
}
