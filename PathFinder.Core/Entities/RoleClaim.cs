using PathFinder.SharedKernel.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Core.Entities
{
    public class RoleClaim
    {
        [Key]
        public int Id { get; set; }
        [StringLength(60, ErrorMessage = "Module cannot exceed 60 characters.")]
        public string Module { get; set; }
        [StringLength(60, ErrorMessage = "Claim Type cannot exceed 60 characters.")] 
        public string? ClaimType { get; set; }
        [StringLength(60, ErrorMessage = "Claim Value cannot exceed 60 characters.")]
        public string? ClaimValue { get; set; }
        public RolePermissionPlatform PermissionPlatform { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; } // Navigation property
    }
}