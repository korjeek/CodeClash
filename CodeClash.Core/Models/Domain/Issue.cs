using CodeClash.Core.Interfaces;

namespace CodeClash.Core.Models;

// подумать над тем, какой тип тестовых данных использовать
public class Issue(Guid id, string description, string name) : IModel<IssueEntity>
{
    public Guid Id { get; } = id;
    public string Description { get; } = description;
    public string Name { get; } = name;


    public IssueEntity GetEntity()
    {
        return new IssueEntity
        {
            Id = Id,
            Description = Description,
            Name = Name
        };
    }

    public static Issue GetModel(IssueEntity issueEntity)
    { 
      return new Issue(issueEntity.Id, issueEntity.Description, issueEntity.Name);
    }
}