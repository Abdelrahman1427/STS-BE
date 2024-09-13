using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PathFinder.Core.Entities
{
    public class ContactUs:Definition
    {
        [Key]
        public int Id { get; set; }
        [StringLength(60, ErrorMessage = "Name cannot exceed 60 characters")]
        public string Name { get; set; }
        [EmailAddress (ErrorMessage = "Invalid email address.")]
        [StringLength(250, ErrorMessage = "Email cannot exceed 250 characters")]
        public string Email { get; set; }
        [StringLength(50, ErrorMessage = "Materials Of Interest cannot exceed 50 characters")]
        public string MaterialsOfInterest { get; set; }
        [StringLength(1000, ErrorMessage = "Message cannot exceed 1000 characters")]
        public string Message { get; set; }
        public int MessageTypeId { get; set; }
        [ForeignKey("MessageTypeId")]
        public MessageType MessageType { get; set; }
    }
}