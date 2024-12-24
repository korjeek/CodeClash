using CodeClash.Application.Extensions;
using CodeClash.Core.Models;
using CodeClash.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeClash.Tests
{
	[TestFixture]
	public class UserExtensionsTests
	{
		[Test]
		public void GetUserDTO_ShouldMapUserToUserDTO()
		{
			var userResult = User.Create(Guid.NewGuid(), "valid@example.com", "securehash", "Valid User");
			Assert.That(userResult.IsSuccess, Is.True);
			var user = userResult.Value;

			var userDTO = user.GetUserDTO();

			Assert.That(userDTO.Email, Is.EqualTo(user.Email));
			Assert.That(userDTO.Name, Is.EqualTo(user.Name));
		}

		[Test]
		public void GetUserEntity_ShouldMapUserToUserEntity()
		{
			var userResult = User.Create(Guid.NewGuid(), "valid@example.com", "securehash", "Valid User");
			Assert.That(userResult.IsSuccess, Is.True);
			var user = userResult.Value;

			user.UpdateRefreshToken("refreshtoken");
			user.UpdateRefreshTokenExpiryTime(DateTime.UtcNow.AddDays(7));
			user.UpdateRoomAdminStatus(true);

			var userEntity = user.GetUserEntity();

			Assert.That(userEntity.Id, Is.EqualTo(user.Id));
			Assert.That(userEntity.Email, Is.EqualTo(user.Email));
			Assert.That(userEntity.Name, Is.EqualTo(user.Name));
			Assert.That(userEntity.PasswordHash, Is.EqualTo(user.PasswordHash));
			Assert.That(userEntity.RefreshToken, Is.EqualTo(user.RefreshToken));
			Assert.That(userEntity.RefreshTokenExpiryTime, Is.EqualTo(user.RefreshTokenExpiryTime));
			Assert.That(userEntity.IsAdmin, Is.EqualTo(user.IsAdmin));
		}

		[Test]
		public void GetUserFromEntity_ShouldMapUserEntityToUser()
		{
			var userEntity = new UserEntity
			{
				Id = Guid.NewGuid(),
				Email = "valid@example.com",
				Name = "Valid User",
				PasswordHash = "securehash",
				RefreshToken = "refreshtoken", 
				RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7),
				IsAdmin = true
			};

			var user = userEntity.GetUserFromEntity();

			Assert.That(user, Is.Not.Null);
			Assert.That(user.Id, Is.EqualTo(userEntity.Id));
			Assert.That(user.Email, Is.EqualTo(userEntity.Email));
			Assert.That(user.Name, Is.EqualTo(userEntity.Name));
			Assert.That(user.PasswordHash, Is.EqualTo(userEntity.PasswordHash));
			Assert.That(user.RefreshToken, Is.Null);
			Assert.That(user.RefreshTokenExpiryTime, Is.EqualTo(default(DateTime)));
			Assert.That(user.IsAdmin, Is.EqualTo(userEntity.IsAdmin));
		}
	}
}
