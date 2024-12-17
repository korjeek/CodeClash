using CSharpFunctionalExtensions;
using static CodeClash.Core.Constants.Constants;

namespace CodeClash.Core.Models;

// подумать над тем, какой тип тестовых данных использовать
public class Issue
{
    public Guid Id { get; private set; }
    public string Description { get; private set; }
    public string Name { get; private set; }

    private Issue(Guid id, string description, string name)
    {
        Id = id;
        Description = description;
        Name = name;
    }

    public static Result<Issue> Create(Guid id, string description, string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > MaxNameLength)
            return Result.Failure<Issue>("Incorrect name.");
        var issue = new Issue(id, description, name);
        return Result.Success(issue);
    }
}