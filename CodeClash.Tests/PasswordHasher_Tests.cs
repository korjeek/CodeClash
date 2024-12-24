using NUnit.Framework;
using CodeClash.Application;

namespace CodeClash.Tests
{
	[TestFixture]
	public class PasswordHasherTests
	{
		[Test]
		public void Generate_ShouldReturnHashedPassword()
		{
			var password = "securePassword123!";
			var hashedPassword = PasswordHasher.Generate(password);
			Assert.That(hashedPassword, Is.Not.Null.And.Not.Empty);
			Assert.That(hashedPassword, Is.Not.EqualTo(password));
		}

		[Test]
		public void Verify_ShouldReturnTrue_ForValidPassword()
		{
			var password = "securePassword123!";
			var hashedPassword = PasswordHasher.Generate(password);

			Assert.That(hashedPassword, Is.Not.Null.And.Not.Empty);

			var result = PasswordHasher.Verify(password, hashedPassword);

			Assert.That(result, Is.True);
		}

		[Test]
		public void Verify_ShouldReturnFalse_ForInvalidPassword()
		{
			var password = "securePassword123!";
			var wrongPassword = "wrongPassword456!";
			var hashedPassword = PasswordHasher.Generate(password);

			Assert.That(hashedPassword, Is.Not.Null.And.Not.Empty);

			var result = PasswordHasher.Verify(wrongPassword, hashedPassword);

			Assert.That(result, Is.False);
		}

		[Test]
		public void Generate_ShouldProduceDifferentHashes_ForSamePassword()
		{
			var password = "securePassword123!";

			var hashedPassword1 = PasswordHasher.Generate(password);
			var hashedPassword2 = PasswordHasher.Generate(password);

			Assert.That(hashedPassword1, Is.Not.EqualTo(hashedPassword2));
		}

		[Test]
		public void Verify_ShouldWork_ForPasswordWithSpecialCharacters()
		{
			var password = "p@$$w0rd!#%";
			var hashedPassword = PasswordHasher.Generate(password);

			Assert.That(hashedPassword, Is.Not.Null.And.Not.Empty);

			var result = PasswordHasher.Verify(password, hashedPassword);

			Assert.That(result, Is.True);
		}

		[Test]
		public void Verify_ShouldWork_ForLongPassword()
		{
			var password = new string('a', 1000);
			var hashedPassword = PasswordHasher.Generate(password);

			Assert.That(hashedPassword, Is.Not.Null.And.Not.Empty);

			var result = PasswordHasher.Verify(password, hashedPassword);

			Assert.That(result, Is.True);
		}
	}
}
