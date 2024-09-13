using System.Security.Claims;

namespace PathFinder.DataTransferObjects.DTOs.Identity.Request
{
    public class CreateClaimsDTO
    {
        public IEnumerable<Claim> Claims { get; set; }
        public string Role { get; set; }
    }
}
