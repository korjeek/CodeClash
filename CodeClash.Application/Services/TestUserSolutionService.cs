using System.Collections.Concurrent;
using CodeClash.Persistence.Entities;
using CodeClash.Persistence.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.Build.Locator;

namespace CodeClash.Application.Services;

public class TestUserSolutionService(RoomsRepository repository)
{
    public readonly ConcurrentDictionary<string, string> startCodeLocations = new()
    {
        ["FindSum"] = "../TestSources/FindSum/SolutionTask.cs",
        ["RomanToInteger"] = "../TestSources/RomanToInteger/SolutionTask.cs",
        ["DeleteDuplicates"] = "../TestSources/DeleteDuplicates/SolutionTask.cs",
        ["LongestCommonPrefix"] = "../TestSources/LongestCommonPrefix/SolutionTask.cs",
        ["ValidParentheses"] = "../TestSources/ValidParentheses/SolutionTask.cs",
        ["Palindrome"] = "../TestSources/Palindrome/SolutionTask.cs",
        ["MergeTwoSortedLists"] = "../TestSources/MergeTwoSortedLists/SolutionTask.cs",
    };

    
    private readonly Dictionary<string, string> issueTestsLocations = new()
    {
        ["FindSum"] = "../TestSources/FindSum/SolutionTaskTests.cs",
        ["RomanToInteger"] = "../TestSources/RomanToInteger/SolutionTaskTests.cs",
        ["DeleteDuplicates"] = "../TestSources/DeleteDuplicates/SolutionTaskTests.cs",
        ["LongestCommonPrefix"] = "../TestSources/LongestCommonPrefix/SolutionTaskTests.cs",
        ["ValidParentheses"] = "../TestSources/ValidParentheses/SolutionTaskTests.cs",
        ["Palindrome"] = "../TestSources/Palindrome/SolutionTaskTests.cs",
        ["MergeTwoSortedLists"] = "../TestSources/MergeTwoSortedLists/SolutionTaskTests.cs"
    };
    
    public async Task<Result<string>> CheckSolution(Guid roomId, string userSolution, string issueName)
    {
        if (!MSBuildLocator.IsRegistered)
            MSBuildLocator.RegisterDefaults();

        var result = await repository.GetRoomById(roomId);;
        if (result is null)
            return Result.Failure<string>("Room does not exist.");
        if (result.Status != RoomStatus.CompetitionInProgress)
            return Result.Failure<string>("Competition hasn't started yet");
        
        var tests = await File.ReadAllTextAsync(issueTestsLocations[issueName]);
        await File.WriteAllTextAsync("../CodeClash.UserSolutionTest/SolutionTaskTests.cs", tests);
        await File.WriteAllTextAsync("../CodeClash.UserSolutionTest/SolutionTask.cs", userSolution);
        
        // var solutionPath = @"C:\FIIT\normalProject\CodeClash";
        var solutionPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../.."));
        var projectName = "CodeClash.UserSolutionTest";
        
        return Result.Success(RuntimeProjectExecutor.HandleProject(projectName, solutionPath));
    }
}