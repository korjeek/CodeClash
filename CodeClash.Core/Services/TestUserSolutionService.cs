using CodeClash.Core.Models;

namespace CodeClash.Core.Services;

public class TestUserSolutionService()
{
    public Task<Answer> CheckSolution(string userSolution)
    {
        // TODO: как добавить общий том для контейнеров?
        // TODO: как обратиться к контейнеру?
        // TODO: как сбилдить проект?
        // TODO: как считать вывод?
        File.WriteAllText("CodeClash.UserSolutionTest/SolutionTask.cs", userSolution);

        throw new NotImplementedException();
    }
}