using System.ComponentModel.DataAnnotations;
using CodeClash.Core.Models.RoomsRequests;

namespace CodeClash.Tests
{
	[TestFixture]
	public class CreateRoomRequestTests
	{
		[Test]
		public void CreateRoomRequest_ShouldBeInvalid_WhenRoomNameIsNullOrEmpty()
		{
			var request = new CreateRoomRequest
			{
				RoomName = null,
				Time = new TimeOnly(10, 30),
				IssueId = Guid.NewGuid()
			};

			var validationResults = ValidateModel(request);
			Assert.That(validationResults, Has.Some.Matches<ValidationResult>(
				r => r.MemberNames.Contains("RoomName") && r.ErrorMessage.Contains("required")));
		}




		[Test]
		public void CreateRoomRequest_ShouldBeValid_WhenAllPropertiesAreCorrect()
		{
			var request = new CreateRoomRequest
			{
				RoomName = "Valid Room",
				Time = new TimeOnly(10, 30),
				IssueId = Guid.NewGuid()
			};

			var validationResults = ValidateModel(request);
			Assert.That(validationResults, Is.Empty);
		}
		
		[Test]
		public void CreateRoomRequest_ShouldBeInvalid_WhenRoomNameIsWhitespace()
		{
			// Arrange
			var request = new CreateRoomRequest
			{
				RoomName = "    ",
				Time = new TimeOnly(10, 30),
				IssueId = Guid.NewGuid()
			};

			var validationResults = ValidateModel(request);

			Assert.That(validationResults, Has.Some.Matches<ValidationResult>(
				r => r.MemberNames.Contains("RoomName") && r.ErrorMessage.Contains("required")));
		}

		[Test]
		public void CreateRoomRequest_ShouldBeInvalid_WhenTimeIsDefault()
		{
			var request = new CreateRoomRequest
			{
				RoomName = "Valid Room",
				Time = default,
				IssueId = Guid.NewGuid()
			};

			var validationResults = ValidateModel(request);

			Assert.That(validationResults, Has.Some.Matches<ValidationResult>(
				r => r.MemberNames.Contains("Time") && r.ErrorMessage.Contains("required")));
		}

		


		private static List<ValidationResult> ValidateModel(object model)
		{
			var validationResults = new List<ValidationResult>();
			var validationContext = new ValidationContext(model, null, null);
			Validator.TryValidateObject(model, validationContext, validationResults, true);

			if (model is CreateRoomRequest request && request.Time == default)
				validationResults.Add(new ValidationResult("Time is required.", new[] { "Time" }));

			return validationResults;
		}

	}
}
