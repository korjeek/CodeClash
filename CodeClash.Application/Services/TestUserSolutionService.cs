using CSharpFunctionalExtensions;
using Microsoft.Build.Locator;

namespace CodeClash.Application.Services;

public class TestUserSolutionService
{
    private readonly Dictionary<string, string> issueTestsLocations = new()
    {
        ["FindSum"] = "../TestSources/FindSum/SolutionTaskTests.cs",
        ["RomanToInteger"] = "../TestSources/RomanToInteger/SolutionTaskTests.cs",
        ["DeleteDuplicates"] = "../TestSources/DeleteDuplicates/SolutionTaskTests.cs",
        ["LongestCommonPrefix"] = "../TestSources/LongestCommonPrefix/SolutionTaskTests.cs",
        ["ValidParentheses"] = "../TestSources/ValidParentheses/SolutionTaskTests.cs",
        ["Palindrome"] = "../TestSources/Palindrome/SolutionTaskTests.cs",
        ["MergeTwoSortedLists"] = "../TestSources/MergeTwoSortedLists/SolutionTaskTests.cs",
    };
    
    
    
    public async Task<Result<string>> CheckSolution(string userSolution, string issueName)
    {
        if (!MSBuildLocator.IsRegistered)
            MSBuildLocator.RegisterDefaults();
        
        var tests = await File.ReadAllTextAsync(issueTestsLocations[issueName]);
        await File.WriteAllTextAsync("../CodeClash.UserSolutionTest/SolutionTaskTests.cs", tests);
        await File.WriteAllTextAsync("../CodeClash.UserSolutionTest/SolutionTask.cs", userSolution);
        
        var solutionPath = @"C:\FIIT\normalProject\CodeClash";
        var projectName = "CodeClash.UserSolutionTest";
        
        return Result.Success(RuntimeProjectExecutor.HandleProject(projectName, solutionPath));
        // просто отправляем этот ответ, только для статистики в конце надо сохранить пользователя и его результат
    }
}