using CodeClash.Core.Services;
using CSharpFunctionalExtensions;
using Microsoft.Build.Locator;

namespace CodeClash.Application.Services;

// TODO: Надо подумать должны ли эти сервисы лежать в Коре, тк они тут нахуй не сдались, по ДДД так не совсем правильно
// Как будто надо все сервисы надо убрать в Application
public class TestUserSolutionService(UsersRepository usersRepository)
{
    private Dictionary<string, string> issueTestsLocations = new()
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
        return RuntimeProjectExecutor.HandleProject(projectName, solutionPath);
        // просто отправляем этот ответ, только для статистики в конце надо сохранить пользователя и его результат
    }
}