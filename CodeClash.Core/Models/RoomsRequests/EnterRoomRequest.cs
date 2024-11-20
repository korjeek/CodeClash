using System.ComponentModel.DataAnnotations;

namespace CodeClash.Core.Models.RoomsRequests;

public class EnterRoomRequest
{
    [Required]
    [Display(Name = "RoomId")]
    public Guid RoomId { get; set; }

    [Required]
    [Display(Name = "UserEmail")]
    public string UserEmail { get; set; } = null!;
}