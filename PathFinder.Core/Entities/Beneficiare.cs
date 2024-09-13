using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Core.Entities
{
    public class Beneficiarie
    {
        [Key]
        public int Id { get; set; }
        [StringLength(250, ErrorMessage = "Full Name cannot exceed 250 characters")]
        public string FullName { get; set; }
        [StringLength(60, ErrorMessage = "Nick Name cannot exceed 60 characters")]
        public string? NickName { get; set; }
        [StringLength(14, ErrorMessage = "ID Number cannot exceed 14 characters")]
        public string IDNumber { get; set; } 
        public DateTime Birthdate { get; set; } 
        public int Age { get; set; } 
        [Range(11,13,ErrorMessage = "Phone number must be between 11 and 13 characters ")]
        public string PhoneNumber { get; set; }
        [Range(11, 13, ErrorMessage = "Another Phone number must be between 11 and 13 characters ")]
        public string ?AnotherPhoneNumber { get; set; }
        [StringLength(50, ErrorMessage = "Workplace cannot exceed 50 characters")]
        public string Workplace { get; set; }
        [StringLength(20, ErrorMessage = "Position cannot exceed 20 characters")]
        public string Position { get; set; }
        public bool IsDisability { get; set; }
        public int GenderId { get; set; } // INT
        public int? DisabilityId { get; set; } // INT
        [ForeignKey("DisabilityId")]
        public Disability? Disability { get; set; }
        public int CityId { get; set; } // INT
        [ForeignKey("CityId")]
        public City City { get; set; }
        public int EducationLevelId { get; set; } // INT
        [ForeignKey("EducationLevelId")]
        public EducationLevel EducationLevel { get; set; }
        public int? IndicatorTransactionId { get; set; } // INT
        [ForeignKey("IndicatorTransactionId")]
        public IndicatorTransaction? IndicatorTransaction { get; set; }
        public ICollection<Engagement>? Engagements { get; set; }
        public ICollection<CourseTransaction>? CourseTransactions { get; set; }
        public ICollection<WorkplaceEnvironment>? WorkplaceEnvironments { get; set; }
    }
}