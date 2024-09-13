namespace PathFinder.DataTransferObjects.DTOs.Identity.Request
{
    public class UserForCreationDTO
    {
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
    }
}