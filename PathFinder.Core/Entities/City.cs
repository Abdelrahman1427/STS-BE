using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Core.Entities
{
    public class City : Definition
    {
        [Key]
        public int Id { get; set; }
        [StringLength(60, ErrorMessage = "En Name cannot exceed 60 characters")]
        public string EnName { get; set; }
        [StringLength(60, ErrorMessage = "Ar Name cannot exceed 60 characters")]
        public string ArName { get; set; }
        public int GovernorateId { get; set; }
        [ForeignKey("GovernorateId")]
        public Governorate Governorate { get; set; }
        public ICollection<Beneficiarie> Beneficiaries  { get; set; }
    }
}