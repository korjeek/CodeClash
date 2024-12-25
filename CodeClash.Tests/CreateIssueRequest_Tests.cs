using CodeClash.Core.Models;
using CodeClash.Core.Constants;

namespace CodeClash.Tests
{
	[TestFixture]
	public class IssueTests
	{
		[Test]
		public void Create_ValidData_ShouldReturnSuccess()
		{
			var id = Guid.NewGuid();
			var description = "Valid description";
			var name = new string('A', Constants.MaxNameLength);

			var result = Issue.Create(id, description, name);

			Assert.Multiple(() =>
			{
				Assert.That(result.IsSuccess, Is.True, "Expected creation to succeed.");
				Assert.That(result.Value, Is.Not.Null, "Result value should not be null.");
				Assert.That(result.Value.Id, Is.EqualTo(id), "ID should match.");
				Assert.That(result.Value.Description, Is.EqualTo(description), "Description should match.");
				Assert.That(result.Value.Name, Is.EqualTo(name), "Name should match.");
			});
		}

		[TestCase("")]
		[TestCase(" ")]
		[TestCase("A string that is way too long and exceeds the maximum allowed length for a name.")]
		public void Create_InvalidName_ShouldReturnFailure(string name)
		{
			var id = Guid.NewGuid();
			var description = "Valid description";

			var result = Issue.Create(id, description, name);

			Assert.Multiple(() =>
			{
				Assert.That(result.IsSuccess, Is.False, "Expected creation to fail.");
				Assert.That(result.Error, Is.EqualTo("Incorrect name."), "Expected error message to match.");
			});
		}

		[Test]
		public void Create_EmptyDescription_ShouldReturnSuccess()
		{
			var id = Guid.NewGuid();
			var description = "";
			var name = new string('A', Constants.MaxNameLength);
			var result = Issue.Create(id, description, name);

			Assert.Multiple(() =>
			{
				Assert.That(result.IsSuccess, Is.True, "Expected creation to succeed.");
				Assert.That(result.Value.Description, Is.EqualTo(description), "Description should be empty.");
			});
		}
	}
}
