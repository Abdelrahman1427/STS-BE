using System.ComponentModel.DataAnnotations;

namespace PathFinder.Core.Entities
{
    public class Certification : Definition
    {
        [Key]
        public int Id { get; set; }
        [StringLength(60, ErrorMessage = "Ar Name cannot exceed 60 characters")]
        public string ArName { get; set; }
        [StringLength(60, ErrorMessage = "En Name cannot exceed 60 characters")]
        public string EnName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<PrivateSector> PrivateSectors { get; set; }
    }
}

