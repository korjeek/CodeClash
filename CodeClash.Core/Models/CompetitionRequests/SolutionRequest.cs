using System.ComponentModel.DataAnnotations;

namespace CodeClash.Core.Models.CompetitionRequests;

public class SolutionRequest
{
    [Required]
    [Display(Name = "UserSolution")]
    public string UserSolution { get; set; } = null!;
}