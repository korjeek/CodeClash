using CodeClash.Core.Models.Domain;
using CodeClash.Application.Extensions;
using CodeClash.Core.Models;
using CodeClash.Persistence.Entities;

namespace CodeClash.Tests
{
	[TestFixture]
	public class RoomExtensionsTests
	{
		[Test]
		public void GetRoomDTOFromRoom_ShouldMapRoomToRoomDTO()
		{
			var issueResult = Issue.Create(Guid.NewGuid(), "Test Description", "Test Issue");
			Assert.That(issueResult.IsSuccess, Is.True);
			var issue = issueResult.Value;

			var roomResult = Room.Create(Guid.NewGuid(), "Test Room", new TimeOnly(12, 0), issue);
			Assert.That(roomResult.IsSuccess, Is.True);
			var room = roomResult.Value;
			room.SetParticipants(new List<User>
			{
				User.Create(Guid.NewGuid(), "user1@example.com", "hash1", "User1").Value,
				User.Create(Guid.NewGuid(), "user2@example.com", "hash2", "User2").Value
			});

			var roomDTO = room.GetRoomDTOFromRoom();

			Assert.That(roomDTO.Id, Is.EqualTo(room.Id.ToString()));
			Assert.That(roomDTO.Name, Is.EqualTo(room.Name));
			Assert.That(roomDTO.Time, Is.EqualTo(room.Time));
			Assert.That(roomDTO.Users.Count, Is.EqualTo(room.Participants.Count));
		}

		[Test]
		public void GetRoomDTOFromRoomEntity_ShouldMapRoomEntityToRoomDTO()
		{
			var roomEntity = new RoomEntity
			{
				Id = Guid.NewGuid(),
				Name = "Entity Room",
				Time = new TimeOnly(12, 0)
			};

			var roomDTO = roomEntity.GetRoomDTOFromRoomEntity();

			Assert.That(roomDTO.Id, Is.EqualTo(roomEntity.Id.ToString()));
			Assert.That(roomDTO.Name, Is.EqualTo(roomEntity.Name));
			Assert.That(roomDTO.Time, Is.EqualTo(roomEntity.Time));
		}

		[Test]
		public void GetRoomEntity_ShouldMapRoomToRoomEntity()
		{
			var issueResult = Issue.Create(Guid.NewGuid(), "Test Description", "Test Issue");
			Assert.That(issueResult.IsSuccess, Is.True);
			var issue = issueResult.Value;

			var roomResult = Room.Create(Guid.NewGuid(), "Test Room", new TimeOnly(12, 0), issue);
			Assert.That(roomResult.IsSuccess, Is.True);
			var room = roomResult.Value;

			var roomEntity = room.GetRoomEntity();

			Assert.That(roomEntity.Id, Is.EqualTo(room.Id));
			Assert.That(roomEntity.Name, Is.EqualTo(room.Name));
			Assert.That(roomEntity.Time, Is.EqualTo(room.Time));
			Assert.That(roomEntity.IssueId, Is.EqualTo(room.Issue.Id));
		}

		[Test]
		public void GetRoomFromEntity_ShouldMapRoomEntityToRoom()
		{
			var issueResult = Issue.Create(Guid.NewGuid(), "Test Description", "Test Issue");
			Assert.That(issueResult.IsSuccess, Is.True);
			var issue = issueResult.Value;

			var roomEntity = new RoomEntity
			{
				Id = Guid.NewGuid(),
				Name = "Entity Room",
				Time = new TimeOnly(12, 0)
			};

			var room = roomEntity.GetRoomFromEntity(issue);

			Assert.That(room.Id, Is.EqualTo(roomEntity.Id));
			Assert.That(room.Name, Is.EqualTo(roomEntity.Name));
			Assert.That(room.Time, Is.EqualTo(roomEntity.Time));
			Assert.That(room.Issue.Id, Is.EqualTo(issue.Id));
		}

		[Test]
		public void IssueCreate_ShouldReturnSuccessForValidInput()
		{
			var id = Guid.NewGuid();
			var description = "Valid Description";
			var name = "Valid Name";

			var issueResult = Issue.Create(id, description, name);

			Assert.That(issueResult.IsSuccess, Is.True);
			var issue = issueResult.Value;
			Assert.That(issue.Id, Is.EqualTo(id));
			Assert.That(issue.Description, Is.EqualTo(description));
			Assert.That(issue.Name, Is.EqualTo(name));
		}

		[Test]
		public void IssueCreate_ShouldReturnFailureForInvalidName()
		{
			var id = Guid.NewGuid();
			var description = "Valid Description";
			var name = string.Empty;

			var issueResult = Issue.Create(id, description, name);

			Assert.That(issueResult.IsFailure, Is.True);
			Assert.That(issueResult.Error, Is.EqualTo("Incorrect name."));
		}

		[Test]
		public void UserCreate_ShouldReturnSuccessForValidInput()
		{
			var id = Guid.NewGuid();
			var email = "valid@example.com";
			var passwordHash = "securehash";
			var name = "Valid Name";

			var userResult = User.Create(id, email, passwordHash, name);

			Assert.That(userResult.IsSuccess, Is.True);
			var user = userResult.Value;
			Assert.That(user.Id, Is.EqualTo(id));
			Assert.That(user.Email, Is.EqualTo(email));
			Assert.That(user.PasswordHash, Is.EqualTo(passwordHash));
			Assert.That(user.Name, Is.EqualTo(name));
		}

		[Test]
		public void UserCreate_ShouldReturnFailureForInvalidEmail()
		{
			var id = Guid.NewGuid();
			var email = string.Empty;
			var passwordHash = "securehash";
			var name = "Valid Name";

			var userResult = User.Create(id, email, passwordHash, name);

			Assert.That(userResult.IsFailure, Is.True);
			Assert.That(userResult.Error, Is.EqualTo("Incorrect email length."));
		}

		[Test]
		public void UserCreate_ShouldReturnFailureForInvalidName()
		{
			var id = Guid.NewGuid();
			var email = "valid@example.com";
			var passwordHash = "securehash";
			var name = string.Empty;

			var userResult = User.Create(id, email, passwordHash, name);

			Assert.That(userResult.IsFailure, Is.True);
			Assert.That(userResult.Error, Is.EqualTo("Incorrect name length."));
		}
	}
}
