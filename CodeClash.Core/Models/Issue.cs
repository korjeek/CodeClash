namespace CodeClash.Core.Models;


// подумать над тем, какой тип тестовых данных использовать
public class Issue(string description)
{
    public Guid Id { get; init; }
    public string Description { get; init; } = description;
    
    // public string Name { get; set; }
    // public Dictionary<Guid, (int[], int[])> TestData { get; set; }
}