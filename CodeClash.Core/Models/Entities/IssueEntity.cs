using CodeClash.Persistence.Interfaces;

namespace CodeClash.Core.Models;


// подумать над тем, какой тип тестовых данных использовать
public class IssueEntity : IEntity
{
    public Guid Id { get; init; }
    public string Description { get; init; }
    public string Name { get; set; }
}