namespace CodeClash.Core.Models;


// подумать над тем, какой тип тестовых данных использовать
public class Issue(string description)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Description { get; set; } = description;
    // public Dictionary<Guid, (int[], int[])> TestData { get; set; }
}