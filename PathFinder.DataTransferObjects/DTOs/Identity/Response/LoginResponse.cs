namespace PathFinder.DataTransferObjects.DTOs.Identity.Response
{
    public class LoginResponseDTO
    {
        public Dictionary<string, string> Claims { get; set; }
        public string Token { get; set; }
        public DateTimeOffset RefreshTokenExpiration { get; set; }
        public string RefreshTokenKey { get; set; }
        public int ProfileStatus { get; set; }
    }
}