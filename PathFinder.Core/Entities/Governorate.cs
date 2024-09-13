using PathFinder.Core.Entities;
using System.ComponentModel.DataAnnotations;

public class Governorate : Definition
{
    [Key]
    public int Id { get; set; }
    [StringLength(60, ErrorMessage = "En Name cannot exceed 60 characters.")]
    public string EnName { get; set; } // English Name, max length 60
    [StringLength(60, ErrorMessage = "Ar Name cannot exceed 60 characters.")]
    public string ArName { get; set; } // Arabic Name, max length 60
    public ICollection<CourseTransaction> CourseTransactions { get; set; }
    public ICollection<NonGovermntalOrgniszation> NonGovermntalOrgniszations { get; set; }
    public ICollection<City> Cities { get; set; }
    public ICollection<PrivateSector> PrivateSectors { get; set; }
    public ICollection<FinancialServiceProvider> FinancialServiceProviders { get; set; }
}