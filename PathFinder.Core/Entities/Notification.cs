using System.ComponentModel.DataAnnotations;

namespace PathFinder.Core.Entities
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [StringLength(60, ErrorMessage = "Sender Name cannot exceed 60 characters.")]
        public string SenderName { get; set; } = null!; // Required, max length 60
        [StringLength(60, ErrorMessage = "Reciver Name cannot exceed 60 characters.")]
        public string ReciverName { get; set; } = null!; // Required, max length 60
        [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters.")]
        public string Message { get; set; } = null!; // Required, max length 500
        [StringLength(50, ErrorMessage = "Message Type cannot exceed 50 characters.")]
        public string MessageType { get; set; } = null!; // Required, max length 50
        public DateTime NotificationDateTime { get; set; } // Required
    }
}