using System.ComponentModel.DataAnnotations;

namespace PathFinder.Core.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [StringLength(60, ErrorMessage = "Name cannot exceed 60 characters.")]
        public string Name { get; set; } // Required, max length 100
        [StringLength(60, ErrorMessage = "Concurrency Stamp cannot exceed 60 characters.")] 
        public string? ConcurrencyStamp { get; set; } // Optional, max length 100
        public ICollection<User> Users { get; set; } // Navigation property
        public bool IsDeleted { get; set; } = false;
    }
}