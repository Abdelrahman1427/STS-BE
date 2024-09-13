namespace PathFinder.DataTransferObjects.DTOs.Account
{
    public class CheckCodeDTO
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public bool isSuccessCode { get; set; }
    }
}
