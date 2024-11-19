using System.ComponentModel.DataAnnotations;

namespace CodeClash.Core.Models.RoomsRequests;

public class QuitRoomRequest
{
    [Required]
    [Display(Name = "RoomId")]
    public string RoomId { get; set; } = null!;
}