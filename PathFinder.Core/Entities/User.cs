using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Core.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(15, MinimumLength = 15, ErrorMessage = "Phone number must be exactly 15 characters long.")]
        public string PhoneNumber { get; set; } // Ensure phone number is exactly 13 characters long
        [EmailAddress]
        [StringLength(256, ErrorMessage = "Email cannot exceed 256 characters.")]
        public string Email { get; set; } // Required, must be a valid email
        [StringLength(100, ErrorMessage = "User Name cannot exceed 100 characters.")]
        public string UserName { get; set; } // Required, max length 100
        [StringLength(256, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"(?=.*[A-Z])(?=.*\d).+", ErrorMessage = "Password must contain at least one uppercase letter and one number.")]
        public string Password { get; set; } // Required, max length 256, must include at least one uppercase letter and one number
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; } // Navigation property
        public int UserTypeId { get; set; }
        [ForeignKey("UserTypeId")]
        public UserType UserType { get; set; } // Navigation property
        public ICollection<AccessToken> AccessTokens { get; set; } = new HashSet<AccessToken>(); // Initialize collection
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>(); // Initialize collection
        public bool IsDeleted { get; set; } = false;
    }
}