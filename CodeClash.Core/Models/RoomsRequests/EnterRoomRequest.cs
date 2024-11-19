using System.ComponentModel.DataAnnotations;

namespace CodeClash.Core.Models.RoomsRequests;

public class EnterRoomRequest
{
    [Required]
    [Display(Name = "RoomId")]
    public string RoomId { get; set; } = null!;
    
    [Required]
    [Display(Name = "Key")]
    public string Key { get; set; } = null!;
}