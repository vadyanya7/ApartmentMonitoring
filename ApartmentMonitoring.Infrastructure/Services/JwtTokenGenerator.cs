using ApartmentMonitoring.Application.Services.Interfaces;
using ApartmentMonitoring.Entity.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApartmentMonitoring.Infrastructure.Services
{
	public class JwtTokenGenerator : IJwtTokenGenerator
	{
		private readonly IConfiguration _configuration;

		public JwtTokenGenerator(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GenerateToken(ApartmentMonitoring.Entity.Entities.User user)
		{
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(ClaimTypes.Role, user.Role.ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.UtcNow.AddDays(7);

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				claims,
				expires: expires,
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);

		}
	}
}
