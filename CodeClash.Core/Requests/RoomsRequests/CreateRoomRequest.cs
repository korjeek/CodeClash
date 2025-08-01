﻿using System.ComponentModel.DataAnnotations;

namespace CodeClash.Core.Requests.RoomsRequests;

public class CreateRoomRequest
{
    [Required]
    [Display(Name = "RoomName")]
    public string RoomName { get; set; }
    
    [Required]
    [RegularExpression(@"^(?:(?:([01]?\d|2[0-3]):)?([0-5]?\d):)?([0-5]?\d)$", ErrorMessage = "Invalid string time")]
    [Display(Name = "Time")]
    public TimeOnly Time { get; set; }
    
    [Required]
    [Display(Name = "IssueId")]
    public Guid IssueId { get; set; }
}