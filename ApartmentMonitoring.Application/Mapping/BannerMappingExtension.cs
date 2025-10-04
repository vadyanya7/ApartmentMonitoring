using ApartmentMonitoring.Contracts.Banner;
using ApartmentMonitoring.Entity.Entities;
using System;


namespace ApartmentMonitoring.Application.Mapping
{
	public static class BannerMappingExtension
	{

		public static BannerDto ToDto(this Banner entity)
		{
			return new BannerDto
			{
				Id = entity.Id,
				Title = entity.Title,
				ImageUrl = entity.ImageUrl,
				RedirectUrl = entity.RedirectUrl,
				Order = entity.Order,
			};
		}
	}
}
