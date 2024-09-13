using System.ComponentModel.DataAnnotations;

namespace PathFinder.Core.Entities
{
    public class Logger : Definition
    {
        [Key]
        [StringLength(60, ErrorMessage = "Id cannot exceed 60 characters.")]
        public string Id { get; set; } // Required, max length 60
        [StringLength(250, ErrorMessage = "Path cannot exceed 250 characters.")]
        public string Path { get; set; } // Required, max length 250
        [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters.")]
        public string Message { get; set; } // Required, max length 500
        [StringLength(50, ErrorMessage = "Type cannot exceed 50 characters.")]
        public string Type { get; set; } // Required, max length 50
    }
}