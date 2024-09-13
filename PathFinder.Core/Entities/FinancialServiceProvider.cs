using PathFinder.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class FinancialServiceProvider
{
    public int Id { get; set; }
    [StringLength(60, ErrorMessage = "Code cannot exceed 60 characters.")]
    public string Code { get; set; } // NVARCHAR, max length 60
    [StringLength(60, ErrorMessage = "Name cannot exceed 60 characters.")]
    public string Name { get; set; } // NVARCHAR, max length 60
    [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
    public string Address { get; set; } // NVARCHAR, max length 250
    [StringLength(60, ErrorMessage = "Latitude cannot exceed 60 characters.")]
    public string Latitude { get; set; } // NVARCHAR, max length 60
    [StringLength(60, ErrorMessage = "Longitude cannot exceed 60 characters.")]
    public string Longitude { get; set; } // NVARCHAR, max length 60
    public DateTime MOUSigingDate { get; set; } // DATETIME
    public DateTime ActivationDate { get; set; } // DATETIME
    [StringLength(60, ErrorMessage = "Place cannot exceed 60 characters.")]
    public string Place { get; set; } // NVARCHAR, max length 60
    public int EngagementLevelId { get; set; } // INT
    [ForeignKey("EngagementLevelId")]
    public EngagementLevel EngagementLevel { get; set; }
    public int MOUId { get; set; } // INT
    [ForeignKey("MOUId")]
    public Documentation Documentation { get; set; }
    [Required]
    public int EnterpriseOriginId { get; set; } // INT
    [ForeignKey("EnterpriseOriginId")]
    public EnterpriseOrigin EnterpriseOrigin { get; set; }
    public int EnterpriseTypeId { get; set; } // INT
    [ForeignKey("EnterpriseTypeId")]
    public EnterpriseType EnterpriseType { get; set; }
    public int CompanySizeId { get; set; } // INT
    [ForeignKey("CompanySizeId")]
    public CompanySize CompanySize { get; set; }
    public int EmployeeId { get; set; } // INT
    [ForeignKey("EmployeeId")]
    public Employee Employee { get; set; }
    public int CompanyStatusId { get; set; } // INT
    [ForeignKey("CompanyStatusId")]
    public CompanyStatus CompanyStatus { get; set; }
    public int CompanySectorId { get; set; } // INT
    [ForeignKey("CompanySectorId")]
    public CompanySector CompanySector { get; set; }
    public int GovernorateId { get; set; } // INT
    [ForeignKey("GovernorateId")]
    public Governorate Governorate { get; set; }
    public int CompanyCategoryId { get; set; } // INT
    [ForeignKey("CompanyCategoryId")]
    public CompanyCategory CompanyCategory { get; set; }
    public ICollection<Engagement> Engagements { get; set; }
}