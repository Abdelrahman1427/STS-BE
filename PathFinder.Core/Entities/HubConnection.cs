using System.ComponentModel.DataAnnotations;

public partial class HubConnection
{
    [Key]
    public int Id { get; set; }
    [StringLength(60, ErrorMessage = "Connection Id cannot exceed 60 characters.")]
    public string ConnectionId { get; set; } = null!; // Required and max length 60
    [StringLength(60, ErrorMessage = "Reciver Name cannot exceed 60 characters.")]
    public string ReciverName { get; set; } = null!; // Required and max length 60
}