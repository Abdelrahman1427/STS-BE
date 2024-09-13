using System.ComponentModel.DataAnnotations;

namespace PathFinder.Core.Entities
{
    public class Documentation: Definition
    {
        [Key]
        public int Id { get; set; }
        [StringLength(60, ErrorMessage = "Title cannot exceed 60 characters")]
        public string Title { get; set; }
        [StringLength(250, ErrorMessage = "Path cannot exceed 250 characters")]
        public string Path { get; set; }
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }
        public ICollection<PrivateSector> PrivateSectors { get; set; }
        public ICollection<FinancialServiceProvider> FinancialServiceProviders { get; set; }
    }
}
