using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using PathFinder.DataTransferObjects.Resources;
using System.Text;

namespace PathFinder.WebApp.CustomTagHelpers
{
    [HtmlTargetElement("form-modal")]
    public class FormModalTagHelper : TagHelper
    {
        public string Title { get; set; }
        public string ActionName { get; set; }
        public string Message { get; set; }
        public string Id { get; set; }
        public string? ButtonName { get; set; } = CoreResources.Yes;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            StringBuilder messageModelHeader = new StringBuilder();
            messageModelHeader.AppendFormat($"<div id='{Id}' class='modal custom-modal fade' role='dialog'>");
            messageModelHeader.AppendFormat("<div class='modal-dialog modal-dialog-centered' role='document'>");
            messageModelHeader.AppendFormat("<div class='modal-content'>");
            messageModelHeader.AppendFormat("<div class='modal-header'>");
            messageModelHeader.AppendFormat($"<h5 class='modal-title'> {ActionName} {Title}</h5>");
            messageModelHeader.AppendFormat("<button type='button' class='close' data-bs-dismiss='modal' aria-label='Close'>");
            messageModelHeader.AppendFormat("<span aria-hidden='true'>&times;</span> </button> </div>");
            messageModelHeader.AppendFormat("<div class='modal-body'>");
            messageModelHeader.AppendFormat($"<form method='post'>");
            messageModelHeader.AppendFormat($"<div class='row'>");
            messageModelHeader.AppendFormat($"<div asp-validation-summary='ModelOnly' class='text-danger'></div>");
            messageModelHeader.AppendFormat($"<div class='form-header'>");
            messageModelHeader.AppendFormat($"<div class=\"text-muted\">{Message} <span class='text-dark' name='owner'></span>{CoreResources.QuestionMark}</div>");
            messageModelHeader.AppendFormat($"<input class='auto-filled-field' hidden name='id' />");
            messageModelHeader.AppendFormat($"</div>");
            messageModelHeader.AppendFormat((await output.GetChildContentAsync()).GetContent());
            messageModelHeader.AppendFormat("</div>");
            messageModelHeader.AppendFormat("<div class='text-center'>");
            messageModelHeader.AppendFormat("<div class=\"row\">");
            messageModelHeader.AppendFormat("<div class=\"col-6\">");
            //messageModelHeader.AppendFormat($"<a href=\"javascript: void(0);\"  class='btn btn-primary continue-btn submit-modal'>{ButtonName}</a>");
            messageModelHeader.AppendFormat($"<a href=\"javascript: void(0);\"  class='btn btn-primary {(ActionName == "Delete" ? "logout-btn" : "continue-btn")} submit-modal'>{ButtonName}</a>");

            messageModelHeader.AppendFormat("</div>");
            messageModelHeader.AppendFormat("<div class=\"col-6\">");
            messageModelHeader.AppendFormat($"<a href=\"javascript:void(0);\" data-bs-dismiss='modal' class='btn btn-primary cancel-btn'>{CoreResources.Cancel}</a>");
            messageModelHeader.AppendFormat("</div>");
            messageModelHeader.AppendFormat("</div> </div>");
            messageModelHeader.AppendFormat($"</form>");
            messageModelHeader.AppendFormat("</div> </div>  </div> </div>");
            
            output.Content.AppendFormat(messageModelHeader.ToString());
        }
    }
}
