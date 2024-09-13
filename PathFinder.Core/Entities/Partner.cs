using System.ComponentModel.DataAnnotations;

namespace PathFinder.Core.Entities
{
    public class Partner : Definition
    {
        [Key]
        public int Id { get; set; }
        [StringLength(60, ErrorMessage = "Ar Name cannot exceed 60 characters.")]
        public string ArName { get; set; } // Required, max length 60
        [StringLength(60, ErrorMessage = "En Name cannot exceed 60 characters.")]
        public string EnName { get; set; } // Required, max length 60
        public ICollection<CourseTransaction> CourseTransactions { get; set; }
    }
}