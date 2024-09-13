using PathFinder.DataTransferObjects.DTOs.Document;
using System.ComponentModel.DataAnnotations;

namespace PathFinder.DataTransferObjects.DTOs.Account
{
    public class RegisterDTO
    {
        public string? Id { get; set; } = null;
        public int? RoleId { get; set; }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? SecondPhoneNumber { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
        public int? GenderId { get; set; }
        public bool? IsTermsConditions { get; set; } = false;
        public long? NationalID { get; set; }
        public int? ProfileStatus { get; set; }

        #region DocumentDTO
        public List<DocumentRequestDTO>? Documents { get; set; } = new List<DocumentRequestDTO>();
        #endregion
    }
}
