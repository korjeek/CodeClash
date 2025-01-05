using System.Collections.Concurrent;
using CodeClash.Persistence.Entities;
using CodeClash.Persistence.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.Build.Locator;

namespace CodeClash.Application.Services;

public class TestUserSolutionService(RoomsRepository roomsRepository)
{
    public readonly ConcurrentDictionary<string, string> startCodeLocations = new()
    {
        ["Find sum"] = "../TestSources/FindSum/SolutionTask.cs",
        ["Roman to integer"] = "../TestSources/RomanToInteger/SolutionTask.cs",
        ["Delete duplicates"] = "../TestSources/DeleteDuplicates/SolutionTask.cs",
        ["Longest common prefix"] = "../TestSources/LongestCommonPrefix/SolutionTask.cs",
        ["Valid parentheses"] = "../TestSources/ValidParentheses/SolutionTask.cs",
        ["Palindrome"] = "../TestSources/Palindrome/SolutionTask.cs",
        ["Merge two sorted lists"] = "../TestSources/MergeTwoSortedLists/SolutionTask.cs",
    };

    
    private readonly Dictionary<string, string> issueTestsLocations = new()
    {
        ["Find sum"] = "../TestSources/FindSum/SolutionTaskTests.cs",
        ["Roman to integer"] = "../TestSources/RomanToInteger/SolutionTaskTests.cs",
        ["Delete duplicates"] = "../TestSources/DeleteDuplicates/SolutionTaskTests.cs",
        ["Longest common prefix"] = "../TestSources/LongestCommonPrefix/SolutionTaskTests.cs",
        ["Valid parentheses"] = "../TestSources/ValidParentheses/SolutionTaskTests.cs",
        ["Palindrome"] = "../TestSources/Palindrome/SolutionTaskTests.cs",
        ["Merge two sorted lists"] = "../TestSources/MergeTwoSortedLists/SolutionTaskTests.cs"
    };
    
    public async Task<Result<string>> CheckSolution(Guid roomId, string userSolution, string issueName)
    {
        if (!MSBuildLocator.IsRegistered)
            MSBuildLocator.RegisterDefaults();

        var result = await roomsRepository.GetRoomById(roomId);;
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