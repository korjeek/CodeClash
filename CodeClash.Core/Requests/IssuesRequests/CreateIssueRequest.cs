using System.ComponentModel.DataAnnotations;

namespace CodeClash.Core.Requests.IssuesRequests;

public class CreateIssueRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public string IssueAdminPassword { get; set; }
}