using ApartmentMonitoring.Contracts.Subscriptions;
using ApartmentMonitoring.Entity.Entities;

namespace ApartmentMonitoring.Application.Mapping
{
	public static class SubscriptionMappingExtensions
	{
		public static SubscriptionDto ToDto(this Subscription entity)
		{
			return new SubscriptionDto
			{
				UserId = entity.User.Id,
				MinPrice = entity.MinPrice,
				MaxPrice = entity.MaxPrice,
				District = entity.District,
				Floor = entity.Floor,
				MaxSize = entity.MaxSize,
				MinSize = entity.MinSize,
				Rooms = entity.Rooms
			};
		}
	}
}
