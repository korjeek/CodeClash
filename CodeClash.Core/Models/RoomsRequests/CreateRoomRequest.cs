using System.ComponentModel.DataAnnotations;

namespace CodeClash.Core.Models.RoomsRequests;

public class CreateRoomRequest
{
    [Required]
    [DataType(DataType.Time)]
    public string Time { get; set; } = null!;
    
    [Required]
    // [Display()]
    public string Issue { get; set; } = null!;
}