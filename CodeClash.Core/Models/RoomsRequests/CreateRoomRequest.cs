using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CodeClash.Core.Models.RoomsRequests;

public class CreateRoomRequest
{
    [Required]
    // [Remote(action: "CheckTime", controller: "RoomController", ErrorMessage = "Invalid string time")]
    [RegularExpression(@"^(?:(?:([01]?\d|2[0-3]):)?([0-5]?\d):)?([0-5]?\d)$", ErrorMessage = "Invalid string time")]
    public TimeOnly Time { get; set; }
    
    [Required]
    public Guid IssueId { get; set; }
    
    [Required]
    [Display(Name = "UserEmail")]
    public string UserEmail { get; set; } = null!;
}