using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Core.Entities
{
    public class PrivateSector
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Code cannot exceed 60 characters.")] 
        public string Code { get; set; } // NVARCHAR
        [StringLength(60, ErrorMessage = "Name cannot exceed 60 characters.")] 
        public string Name { get; set; } // NVARCHAR
        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters.")] 
        public string Address { get; set; } // NVARCHAR
        [StringLength(20, ErrorMessage = "Latitude cannot exceed 20 characters.")] 
        public string Latitude { get; set; } // NVARCHAR
        [StringLength(20, ErrorMessage = "Longitude cannot exceed 20 characters.")] 
        public string Longitude { get; set; } // NVARCHAR
        public DateTime MOUSigingDate { get; set; } // DATETIME
        public DateTime ActivationDate { get; set; } // DATETIME
        public int AssessmentStatusId { get; set; }
        public int CertificationStatusId { get; set; }
        public int ScoreIntervention { get; set; } // INT
        public bool ScoreStatusId { get; set; } // BIT
        public int HiringCount { get; set; } // INT
        public int GenderId { get; set; }
        public int ScoreId { get; set; }
        [ForeignKey("ScoreId")]
        public Score Score { get; set; }
        public int EngagementModalityId { get; set; }
        [ForeignKey("EngagementModalityId")]
        public EngagementModality EngagementModality { get; set; }
        public int CertificationId { get; set; }
        [ForeignKey("CertificationId")]
        public Certification Certification { get; set; }
        public int AssessmentId { get; set; }
        [ForeignKey("AssessmentId")]
        public Assessment Assessment { get; set; }
        public int CourseTransaction { get; set; } // Assuming this is a foreign key, it should be an int
        [ForeignKey("CourseTransaction")]
        public CourseTransaction CourseTransactions { get; set; }
        public int MOUId { get; set; }
        [ForeignKey("MOUId")]
        public Documentation Documentation { get; set; }
        public int EnterpriseOriginId { get; set; }
        [ForeignKey("EnterpriseOriginId")]
        public EnterpriseOrigin EnterpriseOrigin { get; set; }
        public int EnterpriseTypeId { get; set; }
        [ForeignKey("EnterpriseTypeId")]
        public EnterpriseType EnterpriseType { get; set; }
        public int CompanySizeId { get; set; }
        [ForeignKey("CompanySizeId")]
        public CompanySize CompanySize { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public int CompanyStatusId { get; set; }
        [ForeignKey("CompanyStatusId")]
        public CompanyStatus CompanyStatus { get; set; }
        public int CompanySectorId { get; set; }
        [ForeignKey("CompanySectorId")]
        public CompanySector CompanySector { get; set; }
        public int GovernorateId { get; set; }
        [ForeignKey("GovernorateId")]
        public Governorate Governorate { get; set; }
        public int CompanyCategoryId { get; set; }
        [ForeignKey("CompanyCategoryId")]
        public CompanyCategory CompanyCategory { get; set; }
        public int UserTypeId { get; set; }
        [ForeignKey("UserTypeId")]
        public UserType UserType { get; set; }
        public ICollection<WorkplaceEnvironment> WorkplaceEnvironments { get; set; } = new HashSet<WorkplaceEnvironment>(); // Initialize collection
        public ICollection<Policy> Policies { get; set; } = new HashSet<Policy>(); // Initialize collection
        public ICollection<Engagement> Engagements { get; set; } = new HashSet<Engagement>(); // Initialize collection
    }
}