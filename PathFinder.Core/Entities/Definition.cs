using System.ComponentModel.DataAnnotations.Schema;

namespace STS.Core.Entities
{
    public class Definition
    {
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}