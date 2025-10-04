using ApartmentMonitoring.Contracts.Apartments;
using ApartmentMonitoring.Entity.Entities;

namespace ApartmentMonitoring.Application.Mapping
{
	public static class ApartmentMappingExtensions
	{
		public static ApartmentDto ToDto(this Apartment entity)
		{
			return new ApartmentDto
			{
				Price = entity.Price,
				Rooms = entity.Rooms,
				Address = entity.Address,
				Description = entity.Description,
				District = entity.District,
				Floor = entity.Floor,
				Source = entity.Source.ToString()
			};
		}

		public static Apartment ToEntity(this ApartmentDto dto)
		{
			return new Apartment
			{
				Price = dto.Price,
				Rooms = dto.Rooms,
				Address = dto.Address,
				Description = dto.Description,
				District = dto.District,
				Floor = dto.Floor,
				Square = dto.Square
			};
		}
	}
}
