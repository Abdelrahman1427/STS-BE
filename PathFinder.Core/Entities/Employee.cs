using System.ComponentModel.DataAnnotations;

namespace PathFinder.Core.Entities
{
    public class Employee: Definition
    {
        [Key]
        public int Id { get; set; }
        [StringLength(60, ErrorMessage = "Name cannot exceed 60 characters.")]
        public string Name { get; set; }
        public int Gender { get; set; } // Assuming Gender is represented by an integer enum
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(60, ErrorMessage = "Email cannot exceed 60 characters.")]
        public string Email { get; set; }
        [StringLength(60, ErrorMessage = "Position cannot exceed 60 characters.")]
        public string Position { get; set; }
        public ICollection<NonGovermntalOrgniszation> NonGovermntalOrgniszations { get; set; }
        public ICollection<PrivateSector> PrivateSectors { get; set; }
        public ICollection<FinancialServiceProvider> FinancialServiceProviders { get; set; }
    }
}