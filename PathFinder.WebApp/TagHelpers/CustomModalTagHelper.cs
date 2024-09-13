using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace PathFinder.WebApp.CustomTagHelpers
{
    [HtmlTargetElement("custom-modal")]
    public class CustomModalTagHelper:TagHelper
    {
        public string Title { get; set; }//title
        public string Id { get; set; }//action id 
        public string ActionUrl { get; set; }
        public string Class { get; set; }
        public string RepeaterDeleteUrl { get; set; } = "";

        public string ModalSize { get; set; } = "modal-lg";
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            StringBuilder ModelHeader = new StringBuilder();
            ModelHeader.AppendFormat($"<div id=\"{Id}\" action-url=\"{ActionUrl}\" repeater-delete-url=\"{RepeaterDeleteUrl}\" class=\"modal custom-modal {Class} fade\" role=\"dialog\">");
            ModelHeader.AppendFormat($"<div class=\"modal-dialog modal-dialog-scrollable {ModalSize}\">");
            ModelHeader.AppendFormat($"<div class=\"modal-content\">");
            ModelHeader.AppendFormat($"<div class=\"modal-header\">");
            ModelHeader.AppendFormat($"<h5 class=\"modal-title mb-3\">{Title}</h5>");
            ModelHeader.AppendFormat($"<span class='text-dark' name='owner'></span>");
            ModelHeader.AppendFormat("<button type='button' class='close' data-bs-dismiss='modal' aria-label='Close'>");
            ModelHeader.AppendFormat("<span aria-hidden='true'>&times;</span> </button> </div>");
            ModelHeader.AppendFormat($"<div class=\"modal-body\">");
            ModelHeader.AppendFormat((await output.GetChildContentAsync()).GetContent());
            ModelHeader.AppendFormat("</div> </div>  </div> </div>");
            output.Content.AppendFormat(ModelHeader.ToString());
        }
    }
}
