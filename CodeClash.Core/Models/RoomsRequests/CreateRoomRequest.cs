using System.ComponentModel.DataAnnotations;

namespace CodeClash.Core.Models.RoomsRequests;

public class CreateRoomRequest
{
    [Required (ErrorMessage = "Invalid string time")]
    public TimeOnly Time { get; set; }
    
    [Required]
    public Guid IssueId { get; set; }
}