using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Core.Entities
{
    public class NonGovermntalOrgniszation
    {
        public int Id { get; set; }
        [StringLength(60, ErrorMessage = "Name cannot exceed 60 characters.")]
        public string Name { get; set; } // NVARCHAR
        [StringLength(60, ErrorMessage = "Center cannot exceed 60 characters.")]
        public string Center { get; set; } // NVARCHAR
        [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters.")]
        public string Address { get; set; } // NVARCHAR
        [StringLength(60, ErrorMessage = "Latitude cannot exceed 60 characters.")]
        public string Latitude { get; set; } // NVARCHAR
        [StringLength(60, ErrorMessage = "Longitude cannot exceed 60 characters.")]
        public string Longitude { get; set; } // NVARCHAR
        public int AdvertisementNumber { get; set; } // INT
        [StringLength(60, ErrorMessage = "Contact Person cannot exceed 60 characters.")]
        public string ContactPerson { get; set; } // NVARCHAR
        [StringLength(60, ErrorMessage = "Contact Person Title cannot exceed 60 characters.")]
        public string ContactPersonTitle { get; set; } // NVARCHAR
        [StringLength(60, ErrorMessage = "NGO Email cannot exceed 60 characters.")]
        public string NGOEmail { get; set; } // NVARCHAR
        [StringLength(60, ErrorMessage = "Target cannot exceed 60 characters.")]
        public string Target { get; set; } // NVARCHAR
        [StringLength(60, ErrorMessage = "Achieved cannot exceed 60 characters.")]
        public string Achieved { get; set; } // NVARCHAR
        public int GovernorateId { get; set; } // INT
        [ForeignKey("GovernorateId")]
        public Governorate Governorate { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public ICollection<CourseTransaction> CourseTransactions { get; set; }
        public ICollection<CommunityDevlopmentAssosition> CommunityDevlopmentAssositions { get; set; }
    }
}