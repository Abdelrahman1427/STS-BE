using System.ComponentModel.DataAnnotations;

namespace PathFinder.Core.Entities
{
    public class SuccessStories : Definition
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200, ErrorMessage = "Ar Title cannot exceed 200 characters.")] 
        public string ArTitle { get; set; } // Required, max length 200
        [StringLength(200, ErrorMessage = "En Title cannot exceed 200 characters.")] 
        public string EnTitle { get; set; } // Required, max length 200
        [StringLength(1000, ErrorMessage = "En Description cannot exceed 1000 characters.")] 
        public string? EnDescription { get; set; } // Required, max length 1000
        [StringLength(1000, ErrorMessage = "Ar Description cannot exceed 1000 characters.")]
        public string? ArDescription { get; set; } // Required, max length 1000
        [StringLength(250, ErrorMessage = "Image URL cannot exceed 250 characters.")] 
        public string ImageURL { get; set; } // Optional, max length 2048 (standard for URLs)
        [StringLength(250, ErrorMessage = "Video URL cannot exceed 250 characters.")] 
        public string VideoURL { get; set; } // Optional, max length 2048 (standard for URLs)
    }
}