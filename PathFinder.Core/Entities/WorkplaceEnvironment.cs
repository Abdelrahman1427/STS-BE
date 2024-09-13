using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Core.Entities
{
    public class WorkplaceEnvironment : Definition
    {
        [Key]
        public int Id { get; set; }
        [StringLength(60, ErrorMessage = "Ar Name cannot exceed 60 characters.")] // Adjust based on typical length of names
        public string ArName { get; set; } // Required, max length 100
        [StringLength(60, ErrorMessage = "En Name cannot exceed 60 characters.")] // Adjust based on typical length of names
        public string EnName { get; set; } // Required, max length 100
        public int GenderId { get; set; } // Required
        public int PrivateSectorId { get; set; } // Required
        [ForeignKey("PrivateSectorId")]
        public PrivateSector PrivateSector { get; set; } // Navigation property
        public ICollection<Beneficiarie> Beneficiaries { get; set; } = new HashSet<Beneficiarie>(); // Initialize collection
    }
}