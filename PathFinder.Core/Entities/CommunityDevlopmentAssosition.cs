using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Core.Entities
{
    public class CommunityDevlopmentAssosition : Definition
    {
        [Key]
        public int Id { get; set; }
        [StringLength(60, ErrorMessage = "Ar Name cannot exceed 60 characters")]
        public string ArName { get; set; } // NVARCHAR
        [StringLength(60, ErrorMessage = "En Name cannot exceed 60 characters")]
        public string EnName { get; set; } // NVARCHAR
        [StringLength(60, ErrorMessage = "CDA Center cannot exceed 60 characters")]
        public string CDACenter { get; set; } // NVARCHAR
        [StringLength(20, ErrorMessage = "Latitude cannot exceed 20 characters")]
        public string Latitude { get; set; }
        [StringLength(20, ErrorMessage = "Longitude cannot exceed 20 characters")]
        public string Longitude { get; set; } 
        public int NonGovermntalOrgniszationId { get; set; }
        [ForeignKey("NonGovermntalOrgniszationId")]
        public NonGovermntalOrgniszation NonGovermntalOrgniszation { get; set; }
        public ICollection<CourseTransaction> CourseTransactions { get; set; }
    }
}