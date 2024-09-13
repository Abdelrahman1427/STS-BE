using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Core.Entities
{
    public class CourseTransaction
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20, ErrorMessage = "Code cannot exceed 20 characters")]
        public string Code { get; set; } // NVARCHAR
        [StringLength(60, ErrorMessage = "Trainer Name cannot exceed 60 characters")]
        public string TrainerName { get; set; } // NVARCHAR
        public int TrainingDays { get; set; } // INT
        public DateTime CourseTransactionDate { get; set; } // DATETIME
        [StringLength(60, ErrorMessage = "MEL Coordinator cannot exceed 60 characters")]
        public string? MELCoordinator { get; set; } // NVARCHAR
        [StringLength(1000, ErrorMessage = "Place Description cannot exceed 1000 characters")]
        public string? PlaceDescription { get; set; } // NVARCHAR
        [StringLength(20, ErrorMessage = "Latitude cannot exceed 20 characters")]
        public string? Latitude { get; set; } // NVARCHAR
        [StringLength(20, ErrorMessage = "Longitude cannot exceed 20 characters")]
        public string? Longitude { get; set; } // NVARCHAR
        [StringLength(60, ErrorMessage = "CDA Coordinator cannot exceed 60 characters")]
        public string? CDACoordinator { get; set; } // NVARCHAR
        public int? InterventionId { get; set; }
        [ForeignKey("InterventionId")]
        public Intervention? Intervention { get; set; }
        public int? GovernorateId { get; set; } // INT
        [ForeignKey("GovernorateId")]
        public Governorate? Governorate { get; set; }
        public int? NonGovermntalOrgniszationId { get; set; } // INT
        [ForeignKey("NonGovermntalOrgniszationId")]
        public NonGovermntalOrgniszation? NonGovermntalOrgniszation { get; set; }
        public int? PartnerId { get; set; } // INT
        [ForeignKey("PartnerId")]
        public Partner? Partner { get; set; }
        public int? CommunityDevlopmentAssositionId { get; set; } // INT
        [ForeignKey("CommunityDevlopmentAssositionId")]
        public CommunityDevlopmentAssosition? CommunityDevlopmentAssosition { get; set; }
        public int? PrivateSectorId { get; set; }
        [ForeignKey("PrivateSectorId")]// INT
        public PrivateSector? PrivateSector { get; set; }
        public int? IndicatorTransactionId { get; set; }
        [ForeignKey("IndicatorTransactionId")]// INT
        public IndicatorTransaction? IndicatorTransaction { get; set; }
        public int? UserTypeId { get; set; }
        [ForeignKey("UserTypeId")]
        public UserType? UserType { get; set; }
        public ICollection<Beneficiarie>? Beneficiaries { get; set; }
    }
}