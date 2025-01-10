using System.Collections.Concurrent;
using CodeClash.Application.StaticHelpers;
using CodeClash.Core.BusinessLogicHelpers;
using CodeClash.Core.Models.DTOs.SolutionTestResult;
using CodeClash.Persistence.Entities;
using CodeClash.Persistence.Repositories;
using CSharpFunctionalExtensions;
using Microsoft.Build.Locator;

namespace CodeClash.Application.Services;

public class TestUserSolutionService(RoomsRepository roomsRepository, UsersRepository usersRepository)
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

    public async Task<Result<SolutionTestResultDTO>> CheckSolution(Guid roomId, string userSolution, string issueName)
    {
        if (!MSBuildLocator.IsRegistered)
            MSBuildLocator.RegisterDefaults();

        var result = await roomsRepository.GetRoomById(roomId);
        if (result is null)
            return Result.Failure<SolutionTestResultDTO>("Room does not exist.");
        if (result.Status != RoomStatus.CompetitionInProgress)
            return Result.Failure<SolutionTestResultDTO>("Competition hasn't started yet.");

        var tests = await File.ReadAllTextAsync(issueTestsLocations[issueName]);
        await File.WriteAllTextAsync("../CodeClash.UserSolutionTest/SolutionTaskTests.cs", tests);
        await File.WriteAllTextAsync("../CodeClash.UserSolutionTest/SolutionTask.cs", userSolution);

        // var solutionPath = @"C:\FIIT\normalProject\CodeClash";
        var solutionPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../.."));
        var projectName = "CodeClash.UserSolutionTest";

        var stringResult = RuntimeProjectExecutor.HandleProject(projectName, solutionPath);
        return Result.Success(ParseStringResult(stringResult));
    }

    public async Task UpdateUserOverhead(OkResultDTO okResultDto, Guid userId, Guid roomId, TimeOnly leftTime,
        ConcurrentDictionary<Guid, CancellationTokenSource> cancellationTokenDict)
    {
        var userEntity = await usersRepository.GetUserById(userId);
        if (userEntity?.RoomId == null)
            throw new InvalidOperationException("User does not exist in DB or user is not in room.");

        var programWorkingTime = float.Parse(okResultDto.MeanTime);
        if (userEntity.SentTime is not null && userEntity.ProgramWorkingTime <= programWorkingTime)
            return;

        var totalCompetitionTime = (await roomsRepository.GetRoomById(userEntity.RoomId.Value))!.Time;
        var sentTime = TimeOnly.FromTimeSpan(totalCompetitionTime - leftTime);
        var competitionOverhead = CompetitionRuleHelper.GetCompetitionOverhead(sentTime.Second, programWorkingTime);

        userEntity.SentTime = sentTime;
        userEntity.ProgramWorkingTime = programWorkingTime;
        userEntity.CompetitionOverhead = competitionOverhead;
        userEntity.IsSentSolution = true;
        await usersRepository.UpdateUser(userEntity);

        if (await IsAllUsersSentSolution(roomId))
        {
            // await Task.Delay(5000);
            await cancellationTokenDict[roomId].CancelAsync();
        }
    }

    private SolutionTestResultDTO ParseStringResult(string stringResult)
    {
        if (CheckSolutionParser.TryParseResultOk(stringResult, out var okResultDto))
            return new SolutionTestResultDTO { OkResult = okResultDto };
        if (CheckSolutionParser.TryParseErrorResult(stringResult, out var errorResultDto))
            return new SolutionTestResultDTO { ErrorResult = errorResultDto };
        return new SolutionTestResultDTO();
    }

    private async Task<bool> IsAllUsersSentSolution(Guid roomId) =>
        (await usersRepository.GetUsersByRoomId(roomId)).All(u => u.IsSentSolution);
}