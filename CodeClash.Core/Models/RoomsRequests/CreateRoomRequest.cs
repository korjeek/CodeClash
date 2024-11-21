using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace CodeClash.Core.Models.RoomsRequests;

public class CreateRoomRequest
{
    [Required]
    [RegularExpression(@"^(?:(?:([01]?\d|2[0-3]):)?([0-5]?\d):)?([0-5]?\d)$", ErrorMessage = "Invalid string time")]
    [Display(Name = "Time")]
    public TimeOnly Time { get; set; }
    
    [Required]
    [Display(Name = "IssueId")]
    public Guid IssueId { get; set; }
    
    [Required]
    [Display(Name = "UserEmail")]
    public string UserEmail { get; set; } = null!;
}