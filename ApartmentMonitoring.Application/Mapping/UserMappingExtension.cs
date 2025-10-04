using ApartmentMonitoring.Contracts.User;
using ApartmentMonitoring.Entity.Entities;

namespace ApartmentMonitoring.Application.Mapping
{
	public static class UserMappingExtension
	{
		public static UserDto ToDto(this User entity)
		{
			return new UserDto
			{
				Name = entity.Name,
				Email = entity.Email,
				ProfilePhoto = entity.ProfilePhoto,
				Phone = entity.Phone,
				InviteCodes	= new List<string>(entity?.InviteCodes.Select(x => x.Code)),
			};
		}
	}
}
