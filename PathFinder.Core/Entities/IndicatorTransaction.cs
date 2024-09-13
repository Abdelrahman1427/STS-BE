using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Core.Entities
{
    public class IndicatorTransaction
    {
        [Key]
        public int Id { get; set; }
        public int IndicatorId { get; set; } // Required
        [ForeignKey("IndicatorId")]
        public Indicator Indicator { get; set; } // Navigation property to Indicator
        public ICollection<CourseTransaction> CourseTransactions { get; set; }
        public ICollection<Beneficiarie> Beneficiaries { get; set; }
    }
}