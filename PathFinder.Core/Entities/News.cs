using System.ComponentModel.DataAnnotations;

namespace PathFinder.Core.Entities
{
    public class News : Definition 
    {
        public  int  Id { get; set; }
        [StringLength(60, ErrorMessage = "En Title cannot exceed 60 characters.")]
        public  string EnTitle { get; set; }
        [StringLength(60, ErrorMessage = "Ar Title cannot exceed 60 characters.")]
        public string ArTitle { get; set; }
        [StringLength(1000, ErrorMessage = "En Description cannot exceed 1000 characters.")]
        public  string?  EnDescription { get; set; }
        [StringLength(1000, ErrorMessage = "Ar Description cannot exceed 1000 characters.")]
        public string? ArDescription { get; set; }
        [StringLength(250, ErrorMessage = "Image URL cannot exceed 250 characters.")]
        public  string ImageURL { get; set; }
        [StringLength(250, ErrorMessage = "Video URL cannot exceed 250 characters.")]
        public  string  VideoURL { get; set; }
    }
}
