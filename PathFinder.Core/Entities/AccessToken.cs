using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Core.Entities
{
    public class AccessToken
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [StringLength(60, ErrorMessage = "Login Provider cannot exceed 60 characters.")]
        public virtual string? LoginProvider { get; set; }
        [StringLength(60, ErrorMessage = "Value cannot exceed 60 characters.")]
        public virtual string? Value { get; set; }
        [StringLength(60, ErrorMessage = "Name cannot exceed 60 characters.")]
        public string Name { get; set; }
        public virtual DateTime? OccurringDate { get; set; }
    }
}