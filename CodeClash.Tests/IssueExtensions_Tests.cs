using CodeClash.Core.Models;
using CodeClash.Persistence.Entities;
using CodeClash.Application.Extensions;

namespace CodeClash.Tests
{
	[TestFixture]
	public class IssueExtensionsTests
	{
		[Test]
		public void GetIssueDTO_ShouldMapCorrectly()
		{
			var issue = Issue.Create(
				Guid.NewGuid(),
				"Test description",
				"Test name").Value;

			var issueDTO = issue.GetIssueDTO();


			Assert.Multiple(() =>
			{
				Assert.That(issueDTO.Id, Is.EqualTo(issue.Id.ToString()), "ID should match.");
				Assert.That(issueDTO.Description, Is.EqualTo(issue.Description), "Description should match.");
				Assert.That(issueDTO.Name, Is.EqualTo(issue.Name), "Name should match.");
			});
		}

		[Test]
		public void GetIssueEntity_ShouldMapCorrectly()
		{
			var issue = Issue.Create(
				Guid.NewGuid(),
				"Test description",
				"Test name").Value;

			var issueEntity = issue.GetIssueEntity();

			Assert.Multiple(() =>
			{
				Assert.That(issueEntity.Id, Is.EqualTo(issue.Id), "ID should match.");
				Assert.That(issueEntity.Description, Is.EqualTo(issue.Description), "Description should match.");
				Assert.That(issueEntity.Name, Is.EqualTo(issue.Name), "Name should match.");
			});
		}

		[Test]
		public void GetIssueFromEntity_ShouldMapCorrectly()
		{
			var id = Guid.NewGuid();
			var description = "Test description";
			var name = "Test name";
			var issueEntity = new IssueEntity
			{
				Id = id,
				Description = description,
				Name = name
			};

			var issue = issueEntity.GetIssueFromEntity();

			Assert.Multiple(() =>
			{
				Assert.That(issue.Id, Is.EqualTo(issueEntity.Id), "ID should match.");
				Assert.That(issue.Description, Is.EqualTo(issueEntity.Description), "Description should match.");
				Assert.That(issue.Name, Is.EqualTo(issueEntity.Name), "Name should match.");
			});
		}
	}
}
