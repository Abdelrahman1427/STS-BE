using Microsoft.AspNetCore.Http;

namespace PathFinder.DataTransferObjects.DTOs.Document
{
    public class DocumentTitleDTO
    {
        public string? Title { get; set; }
        public string? FileName { get; set; }
        public IFormFile? File { get; set; }
    }
}
