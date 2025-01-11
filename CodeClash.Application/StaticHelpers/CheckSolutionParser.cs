using CodeClash.Core.Models.DTOs.SolutionTestResult;

namespace CodeClash.Application.StaticHelpers;

public static class CheckSolutionParser
{
    public static bool TryParseResultOk(string result, out OkResultDTO? okResultDto)
    {
        var splitResult = result.Split("::");
        okResultDto = null;
        if (splitResult[0] != "OK")
            return false;
        okResultDto = new OkResultDTO
        {
            MeanTime = splitResult[1],
            PassedTestsCount = splitResult[2]
        };
        return true;
    }

    public static bool TryParseErrorResult(string result, out ErrorResultDTO? errorResultDto)
    {
        var splitResult = result.Split("::");
        errorResultDto = null;
        switch (splitResult[0])
        {
            case "OK":
                return false;
            case "FAIL":
                errorResultDto = new ErrorResultDTO { FailedTestError = GetFailedTestErrorDto(splitResult) };
                break;
            default:
                errorResultDto = new ErrorResultDTO { CompileError = GetCompileErrorDto(splitResult) };
                break;
        }
        
        return true;
    }

    private static FailedTestErrorDTO GetFailedTestErrorDto(string[] splitResult) =>
        new FailedTestErrorDTO
        {
            FailedTestName = splitResult[1],
            PassedTestCount = splitResult[2],
            AllTestsCount = splitResult[3],
        };

    private static CompileErrorDTO GetCompileErrorDto(string[] splitResult) =>
        new CompileErrorDTO { CompileError = splitResult[0] };
}