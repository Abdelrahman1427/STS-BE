namespace PathFinder.DataTransferObjects.DTOs.Identity.Response
{
    public class UserResponse
    {
        public bool IsActive { get; set; } = true;
        public string? RestPasswordToken { get; set; }
        public DateTimeOffset? RestPasswordTokenSendDate { get; set; }
        public string? PasswordHash { get; set; }
    }
}