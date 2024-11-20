using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CodeClash.Core.Models.RoomsRequests;

public class CreateRoomRequest
{
    [Required]
    [DataType(DataType.Time)]
    [Remote(action: "CheckTime", controller: "RoomController", ErrorMessage = "Invalid string time")]
    public TimeOnly Time { get; set; }
    
    [Required]
    // [Display()]
    public Issue Issue { get; set; } = null!;
}