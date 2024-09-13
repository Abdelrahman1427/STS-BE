using Microsoft.DotNet.PlatformAbstractions;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.DataTransferObjects.DTOs.Identity.Request
{
    public class LoginRequestMobileDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string? FCMToken { get; set; }
        public Platform? Platform { get; set; }
    }
}
