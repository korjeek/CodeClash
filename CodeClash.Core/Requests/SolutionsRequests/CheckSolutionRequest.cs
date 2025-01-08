namespace CodeClash.Core.Requests.SolutionsRequests;

public class CheckSolutionRequest
{
    public Guid RoomId { get; } 
    public string Solution { get; }  
    public string IssueName { get; }
    public TimeOnly LeftTime { get; }
}