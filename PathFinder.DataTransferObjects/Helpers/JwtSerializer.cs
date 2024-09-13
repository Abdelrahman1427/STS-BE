

namespace PathFinder.DataTransferObjects.Helpers
{
    public class JwtSerializer
    {
        public string Sub { get; set; }
        public string Jti { get; set; }
        public string Iat { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}
