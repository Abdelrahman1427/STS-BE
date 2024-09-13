using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Core.Entities
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [StringLength(1000, ErrorMessage = "Token cannot exceed 1000 characters.")]
        public string Token { get; set; }
        public DateTimeOffset CreatedOn { get; set; } 
        public DateTimeOffset ExpiredOn { get; set; } 
        public DateTimeOffset? RevokedOn { get; set; } // Nullable DateTime, can be null if not revoked
    }
}