namespace CodeClash.Core.Requests.SolutionsRequests;

public class CheckSolutionRequest
{
    public string Solution { get; set; }  
    public string IssueName { get; set; }
    public TimeOnly LeftTime { get; set; }
}