using System.Runtime.InteropServices.ComTypes;
using CodeClash.Core.Interfaces;

namespace CodeClash.Core.Models;

// подумать над тем, какой тип тестовых данных использовать
public class Issue(string description, string name) : IModel<IssueEntity>
{
    public Guid Id { get; private init; }
    public string Description { get; private init; } = description;
    public string Name { get; private init; } = name;


    public IssueEntity GetEntity()
    {
        return new IssueEntity
        {
            Id = Id,
            Description = Description
        };
    }

    public Issue GetModel(IssueEntity issueEntity) => new Issue(issueEntity.Id, issueEntity.Description, issueEntity.Name);
}