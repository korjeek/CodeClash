namespace CodeClash.Core.Models;


// подумать над тем, какой тип тестовых данных использовать
public record Problem(Guid Id, string Description, Dictionary<Guid, (int[], int[])> TestData);