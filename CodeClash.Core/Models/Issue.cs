namespace CodeClash.Core.Models;


// подумать над тем, какой тип тестовых данных использовать
public record Issue(Guid Id, string Description, Dictionary<Guid, (int[], int[])> TestData);