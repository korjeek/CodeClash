using System.ComponentModel.DataAnnotations;

namespace CodeClash.Core.Requests.SolutionsRequests;

public class CheckSolutionRequest
{
    [Required]
    public string Solution { get; set; }  
    [Required]
    public string IssueName { get; set; }
    [Required]
    public TimeOnly LeftTime { get; set; }
}