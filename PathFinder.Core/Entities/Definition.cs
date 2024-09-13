using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Core.Entities
{
    public class Definition
    {
        public int? CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public User? CreatedByUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [ForeignKey("UpdatedBy")]
        public User? UpdatedByUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}