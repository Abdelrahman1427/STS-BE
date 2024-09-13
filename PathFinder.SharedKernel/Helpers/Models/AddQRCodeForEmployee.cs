

namespace STS.Common.Helpers.Models
{
    public class AddQRCodeForEmployee
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string CompanyName { get; set; }
        public string? Email { get; set; }
        public string? Job { get; set; }
        public string? Mobile { get; set; }
        public string? LinkedIn { get; set; }
        public string? Title { get; set; }

        public string? VCFURL { get; set; }
    }
}
