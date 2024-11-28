using System.ComponentModel.DataAnnotations;

namespace CodeClash.Core.Models.RoomsRequests;

public class EnterQuitRoomRequest
{
    [Required]
    [Display(Name = "RoomId")]
    public Guid RoomId { get; set; }
}