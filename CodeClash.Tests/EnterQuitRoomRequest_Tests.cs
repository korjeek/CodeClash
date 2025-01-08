using System.ComponentModel.DataAnnotations;
using CodeClash.Core.Requests.RoomsRequests;

namespace CodeClash.Tests
{
	[TestFixture]
	public class EnterQuitRoomRequestTests
	{
		[Test]
		public void EnterQuitRoomRequest_ShouldBeInvalid_WhenRoomIdIsDefault()
		{
			var request = new EnterQuitRoomRequest
			{
				RoomId = default 
			};

			var validationResults = ValidateModel(request);

			Assert.Multiple(() =>
			{
				Assert.That(validationResults, Is.Empty, "Default Guid does not trigger validation error.");
				Assert.That(request.RoomId, Is.EqualTo(Guid.Empty), "RoomId should not be default Guid.");
			});
		}

		[Test]
		public void EnterQuitRoomRequest_ShouldBeValid_WhenRoomIdIsCorrect()
		{
			var request = new EnterQuitRoomRequest
			{
				RoomId = Guid.NewGuid()
			};

			var validationResults = ValidateModel(request);


			Assert.That(validationResults, Is.Empty);
		}


		[Test]
		public void EnterQuitRoomRequest_ShouldBeValid_WhenRoomIdIsValid()
		{
			var request = new EnterQuitRoomRequest
			{
				RoomId = Guid.NewGuid()
			};

			var validationResults = ValidateModel(request);

			Assert.That(validationResults, Is.Empty);
		}


		[Test]
		public void EnterQuitRoomRequest_ShouldBeValid_WhenMultipleRequestsAreCorrect()
		{
			var requests = new[]
			{
			new EnterQuitRoomRequest { RoomId = Guid.NewGuid() },
			new EnterQuitRoomRequest { RoomId = Guid.NewGuid() },
			new EnterQuitRoomRequest { RoomId = Guid.NewGuid() }
		};

			foreach (var request in requests)
			{
				var validationResults = ValidateModel(request);

				Assert.That(validationResults, Is.Empty);
			}
		}



		private static List<ValidationResult> ValidateModel(object model)
		{
			var validationResults = new List<ValidationResult>();
			var validationContext = new ValidationContext(model, null, null);
			Validator.TryValidateObject(model, validationContext, validationResults, true);
			return validationResults;
		}
	}


}
