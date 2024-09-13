namespace PathFinder.DataTransferObjects.DTOs.Identity.Response
{
    public class CheckCodeResponse
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public bool IsSuccessCode { get; set; }
    }
}
