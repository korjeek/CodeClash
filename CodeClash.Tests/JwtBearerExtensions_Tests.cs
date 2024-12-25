using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CodeClash.Application.Extensions;
using Microsoft.Extensions.Configuration;
using CodeClash.Core.Models;

namespace CodeClash.Tests
{
	[TestFixture]
	public class JwtBearerExtensionsTests
	{
		private IConfiguration _configuration;

		[SetUp]
		public void Setup()
		{
			var inMemorySettings = new Dictionary<string, string>
			{
				{ "Jwt:Secret", "SuperSecretKeyThatIsAtLeast32Characters!" },
				{ "Jwt:Issuer", "TestIssuer" },
				{ "Jwt:Audience", "TestAudience" },
				{ "Jwt:Expire", "3600" }
			};

					_configuration = new ConfigurationBuilder()
						.AddInMemoryCollection(inMemorySettings)
						.Build();
		}

		[Test]
		public void CreateClaims_ShouldReturnValidClaims()
		{
			var userResult = User.Create(
				Guid.NewGuid(),
				"test@example.com",
				"hashedPassword",
				"Test User",
				isAdmin: true
			);

			Assert.That(userResult.IsSuccess, Is.True, "User creation failed unexpectedly.");
			var user = userResult.Value;

			var claims = user.CreateClaims();

			Assert.Multiple(() =>
			{
				Assert.That(claims, Has.Count.EqualTo(4), "Expected 4 claims.");
				Assert.That(claims, Has.Exactly(1).Matches<Claim>(c => c.Type == ClaimTypes.NameIdentifier && c.Value == user.Id.ToString()));
				Assert.That(claims, Has.Exactly(1).Matches<Claim>(c => c.Type == ClaimTypes.Name && c.Value == user.Name));
				Assert.That(claims, Has.Exactly(1).Matches<Claim>(c => c.Type == ClaimTypes.Email && c.Value == user.Email));
				Assert.That(claims, Has.Exactly(1).Matches<Claim>(c => c.Type == JwtRegisteredClaimNames.Jti));
			});
		}

		[Test]
		public void CreateJwtToken_ShouldReturnValidJwtToken()
		{
			// Arrange
			var userResult = User.Create(
				Guid.NewGuid(),
				"test@example.com",
				"hashedPassword",
				"Test User",
				isAdmin: false
			);

			Assert.That(userResult.IsSuccess, Is.True, "User creation failed unexpectedly.");
			var user = userResult.Value;

			var claims = user.CreateClaims();

			var jwtToken = claims.CreateJwtToken(_configuration);

			Assert.Multiple(() =>
			{
				Assert.That(jwtToken, Is.Not.Null, "JWT token should not be null.");
				Assert.That(jwtToken.Issuer, Is.EqualTo(_configuration["Jwt:Issuer"]), "Issuer should match.");
				Assert.That(jwtToken.Audiences, Contains.Item(_configuration["Jwt:Audience"]), "Audience should match.");
				Assert.That(jwtToken.ValidTo, Is.GreaterThan(DateTime.UtcNow), "Expiration should be in the future.");
			});
		}


		[Test]
		public void GenerateRefreshToken_ShouldReturnBase64String()
		{
			var refreshToken = JwtBearerExtensions.GenerateRefreshToken();

			Assert.Multiple(() =>
			{
				Assert.That(refreshToken, Is.Not.Null.Or.Empty, "Refresh token should not be null or empty.");
				Assert.DoesNotThrow(() => Convert.FromBase64String(refreshToken), "Refresh token should be valid Base64.");
			});
		}

		[Test]
		public void GetPrincipalFromExpiredToken_ShouldReturnValidPrincipal()
		{
			var userResult = User.Create(
				Guid.NewGuid(),
				"test@example.com",
				"hashedPassword",
				"Test User",
				isAdmin: false
			);

			Assert.That(userResult.IsSuccess, Is.True, "User creation failed unexpectedly.");
			var user = userResult.Value;

			var claims = user.CreateClaims();
			var jwtToken = claims.CreateJwtToken(_configuration);
			var handler = new JwtSecurityTokenHandler();
			var tokenString = handler.WriteToken(jwtToken);

			var principal = _configuration.GetPrincipalFromExpiredToken(tokenString);

			Assert.Multiple(() =>
			{
				Assert.That(principal, Is.Not.Null, "Principal should not be null.");
				Assert.That(principal.Identity, Is.Not.Null.And.InstanceOf<ClaimsIdentity>(), "Identity should be ClaimsIdentity.");
				Assert.That(principal.Claims, Is.Not.Empty, "Principal should have claims.");
				Assert.That(principal.Claims, Has.Exactly(1).Matches<Claim>(c => c.Type == ClaimTypes.NameIdentifier && c.Value == user.Id.ToString()));
			});
		}
	}
}
