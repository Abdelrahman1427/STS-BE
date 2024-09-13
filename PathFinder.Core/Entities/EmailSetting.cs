using PathFinder.SharedKernel.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Core.Entities
{
    public class EmailSetting
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public MailServer MailServer { get; set; }
        [StringLength(60, ErrorMessage = "ServerName cannot exceed 60 characters.")]
        public string ServerName { get; set; }
        [Range(1, 65535, ErrorMessage = "Port number must be between 1 and 65535.")]
        public int Port { get; set; }
        public Encryption Encryption { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(60, ErrorMessage = "Email cannot exceed 60 characters.")]
        public string Email { get; set; }
        [StringLength(60, ErrorMessage = "Password cannot exceed 60 characters.")]
        public string Password { get; set; }
    }
}