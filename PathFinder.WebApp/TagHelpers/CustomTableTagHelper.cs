using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using PathFinder.WebApp.Models;
using PathFinder.DataTransferObjects.Resources;
using System.Text;

namespace PathFinder.WebApp.CustomTagHelpers
{

    [HtmlTargetElement("custom-table")]
    public class CustomTableTagHelper : TagHelper
    {
        public string Title { get; set; }
        public bool IsAddable { get; set; } = true;
        public bool HasGridUITable { get; set; } = false;
        public bool CanExport { get; set; } = false;
        public string ExpoertExcelMessage { get; set; } = "";
        public string ExpoertExcelURL { get; set; } = "";
        public string ExpoertCSVMessage { get; set; } = "";
        public string ExpoertCsvURL { get; set; } = "";
        public string ExportFunctionName { get; set; } = "Export";
        public bool CanImport { get; set; } = false;
        public string ImportMessage { get; set; } = "";
        public string ImportURL { get; set; } = "";
        public string AddUrl { get; set; }
        public string AddModalId { get; set; } = "add-modal";
        public string TableClasses { get; set; }
        public string DivTableClasses { get; set; }
        public bool IsPage { get; set; }
        public List<BreadCrumb> BreadCrumbs { get; set; } = null;
        public bool IsBreadCrumbsTitleShown { get; set; } = true;

        public string BreadCrumbsTitle { get; set; } = "";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            StringBuilder table = new StringBuilder();
            table.AppendFormat("<div class=\"page-header\">");
            table.AppendFormat("<div class=\"row align-items-center\">");
            table.AppendFormat("<div class=\"col\">");
            table.AppendFormat($"<h3 class=\"page-title\">{Title}</h3>");
            if (BreadCrumbs != null)
            {
                table.AppendFormat("<ul class=\"breadcrumb\">");
                foreach (var item in BreadCrumbs)
                {
                    if (item.URL != null)
                        table.AppendFormat($"<li class=\"breadcrumb-item\"><a href=\"{item.URL}\">{item.Name}</a></li>");
                    else
                        table.AppendFormat($"<li class=\"breadcrumb-item\">{item.Name}</li>");
                }
                if (IsBreadCrumbsTitleShown)
                    table.AppendFormat($"<li class=\"breadcrumb-item active\">{Title}</li>");
                table.AppendFormat("</ul>");
                if(!string.IsNullOrEmpty(BreadCrumbsTitle))
                    table.AppendFormat($"<h5 class=\"mt-2\">{BreadCrumbsTitle}</h5>");
            }
            table.AppendFormat("</div>");

            table.AppendFormat("<div class=\"col-auto float-end ms-auto\">");
            if (IsAddable)
            {
                if (!IsPage)
                {
                    if (AddUrl != null)
                        table.AppendFormat($"<a class=\"btn add-btn ajax-modal\" modal-id=\"{AddModalId}\" action-url=\"/{AddUrl}\"><i class=\"fa fa-plus\"></i> {CoreResources.Add}</a>");
                    else
                        table.AppendFormat($"<a href=\"#\" class=\"btn add-btn\" data-bs-toggle=\"modal\" data-bs-target=\"#{AddModalId}\"><i class=\"fa fa-plus\"></i> {CoreResources.Add}</a>");
                }
                else
                    table.AppendFormat($"<a href=\"{AddUrl}\" class=\"btn add-btn\"><i class=\"fa fa-plus\"></i> {CoreResources.Add} {Title}</a>");

            }
            table.AppendFormat("<div class=\"view-icons\">");
            if (HasGridUITable)
            {
                table.AppendFormat("<a href=\"#\" onClick=\"GetDataTable()\" class=\"list-view btn btn-link\"><i class=\"fa fa-bars\"></i></a>");
                table.AppendFormat("<a href=\"#\" onClick=\"GetGirdTable()\" class=\"grid-view btn btn-link active\"><i class=\"fa fa-th\"></i></a>");

            }
            if (CanExport)
            {
                table.AppendFormat($"<a href=\"#\" onClick=\"{ExportFunctionName}('{ExpoertExcelURL}')\" class=\"list-view btn btn-link \" data-bs-toggle=\"tooltip\" title=\"{ExpoertExcelMessage}\"><i class=\"fa fa-upload\"></i></a>");
                table.AppendFormat($"<a href=\"#\" onClick=\"{ExportFunctionName}('{ExpoertCsvURL}')\" class=\"list-view btn btn-link\" data-bs-toggle=\"tooltip\" title=\"{ExpoertCSVMessage}\"><i class=\"fa fa-file\"></i></a>");
            }
            if (CanImport)
            {
                table.AppendFormat($"<a href=\"{ImportURL}\" class=\"list-view btn btn-link \" data-bs-toggle=\"tooltip\" title=\"{ImportMessage}\"><i class=\"fa fa-download\"></i></a>");
            }
            table.AppendFormat("</div>");

            table.AppendFormat("</div>");

            table.AppendFormat("</div>");
            table.AppendFormat("</div>");
            table.AppendFormat("<div class=\"mb-3\">");
            table.AppendFormat((await output.GetChildContentAsync()).GetContent());
            table.AppendFormat("</div>");
            table.AppendFormat("<div class=\"row\">");
            table.AppendFormat("<div class=\"col-md-12\">");
            table.AppendFormat($"<div class=\"table-responsive {DivTableClasses}\">");
            table.AppendFormat($"<table lang='{System.Globalization.CultureInfo.CurrentCulture.Name}' class=\"table table-striped custom-table mb-0 datatable w-100 {TableClasses} \">");
            table.AppendFormat("</table>");
            table.AppendFormat("</div>");
            table.AppendFormat("</div>");
            table.AppendFormat("</div>");
            output.Content.AppendFormat(table.ToString());
        }
    }
}
