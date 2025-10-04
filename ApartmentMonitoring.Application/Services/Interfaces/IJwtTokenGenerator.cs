using ApartmentMonitoring.Entity.Entities;

namespace ApartmentMonitoring.Application.Services.Interfaces
{
	public interface IJwtTokenGenerator
	{
		string GenerateToken(User user);
	}
}
