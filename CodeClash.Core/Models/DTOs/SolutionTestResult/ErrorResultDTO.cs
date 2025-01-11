namespace CodeClash.Core.Models.DTOs.SolutionTestResult;

public class ErrorResultDTO
{
    public CompileErrorDTO? CompileError { get; set; }
    public FailedTestErrorDTO? FailedTestError { get; set; }
}