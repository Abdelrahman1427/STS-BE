using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Core.Entities
{
    public class Intervention: Definition
    {
        [Key]
        public int Id { get; set; }
        [StringLength(60, ErrorMessage = "Code cannot exceed 60 characters.")]
        public string Code { get; set; } // Required, max length 60
        [StringLength(60, ErrorMessage = "Ar Name cannot exceed 60 characters.")]
        public string ArName { get; set; } // Required, max length 60
        [StringLength(60, ErrorMessage = "En Name cannot exceed 60 characters.")]
        public string EnName { get; set; } // Required, max length 60
        public int ObjectiveId { get; set; } // Required
        [ForeignKey("ObjectiveId")]
        public Objective Objective { get; set; } // Navigation property
        public ICollection<CourseTransaction> CourseTransactions { get; set; }
    }
}