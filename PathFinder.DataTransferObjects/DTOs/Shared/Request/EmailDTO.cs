using Microsoft.AspNetCore.Http;

namespace PathFinder.DataTransferObjects.DTOs.Shared.Request
{
    public class EmailDTO
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
