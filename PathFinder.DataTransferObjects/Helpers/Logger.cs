using System.ComponentModel.DataAnnotations;

namespace STS.DataTransferObjects.Helpers
{
    public class Logger 
    {
        [Required(ErrorMessage = "Path is Required")]
        public string Path { get; set; }
        [Required(ErrorMessage = "Message is Required")]
        public string Message { get; set; }
        [Required(ErrorMessage = "type is Required")]
        public string type { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
