using System.ComponentModel.DataAnnotations;

namespace CodeClash.Core.Requests.RoomsRequests;

public class EnterQuitRoomRequest
{
    [Required]
    [Display(Name = "RoomId")]
    public Guid RoomId { get; set; }
}