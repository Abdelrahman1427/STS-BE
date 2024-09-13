
namespace PathFinder.DataTransferObjects.DTOs.Identity.Request
{
    public class RefreshTokenDTO
    {
        public DateTimeOffset RefreshTokenExpiration { get; set; }
        public string RefreshTokenKey { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}
