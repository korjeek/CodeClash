namespace CodeClash.Core.Models.DTOs.SolutionTestResult;

public class FailedTestErrorDTO
{
    public string FailedTestName { get; set; }
    public string PassedTestCount { get; set; }
    public string AllTestsCount { get; set; }
}