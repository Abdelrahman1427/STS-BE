using Microsoft.AspNetCore.Razor.TagHelpers;
using PathFinder.DataTransferObjects.Resources;
using System.Text;

namespace PathFinder.WebApp.TagHelpers
{
    [HtmlTargetElement("form-footer")]
    public class FormFooterTagHelper : TagHelper
    {
        public string? ButtonName { get; set; } = CoreResources.Submit;
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            StringBuilder FormFooter = new StringBuilder();
            FormFooter.AppendFormat("<div class='submit-section'>");
            FormFooter.AppendFormat("<div class=\"row\">");
            FormFooter.AppendFormat("<label class=\"text-danger hidden error-label mb-4\"></label>");
            FormFooter.AppendFormat("</div>");
            FormFooter.AppendFormat("<div class=\"row\">");
            FormFooter.AppendFormat("<div class=\"col-6\">");
            FormFooter.AppendFormat($"<button type='submit' class='btn btn-primary continue-btn submit-modal w-100'>{ButtonName}</button>");
            FormFooter.AppendFormat("</div>");
            FormFooter.AppendFormat("<div class=\"col-6\">");
            FormFooter.AppendFormat($"<a href=\"javascript:void(0);\" data-bs-dismiss='modal' class='btn btn-primary cancel-btn'>{CoreResources.Cancel}</a>");
            FormFooter.AppendFormat("</div>");
            FormFooter.AppendFormat("</div> </div>");
            output.Content.AppendFormat(FormFooter.ToString());
        }
    }
}
