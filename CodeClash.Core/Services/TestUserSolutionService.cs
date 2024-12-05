using CodeClash.Core.Models;

namespace CodeClash.Core.Services;

public class TestUserSolutionService()
{
    public async Task CheckSolution(string userSolution)
    {
        // TODO: как добавить общий том для контейнеров?
        // TODO: как обратиться к контейнеру?
        // TODO: как сбилдить проект?
        // TODO: как считать вывод?
        File.WriteAllText("CodeClash.UserSolutionTest/SolutionTask.cs", userSolution);
        
    }
}