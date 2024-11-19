using System.ComponentModel.DataAnnotations;

namespace CodeClash.Core.Models.RoomsRequests;

public class StartCompetitionRequest
{
    [Required]
    [Display(Name = "RoomId")]
    public string RoomId { get; set; } = null!;
}