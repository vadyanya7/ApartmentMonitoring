using ApartmentMonitoring.Contracts.Notifications;
using ApartmentMonitoring.Infrastructure;

namespace ApartmentMonitoring.Application.Mapping
{
	public static class NotificationMappingExtension
	{
		public static NotificationDto ToDto(this Notification entity)
		{
			return new NotificationDto
			{
				NotificationId=entity.Id,
				UserId = entity.UserId,
				CreatedAt = entity.CreatedAt,
				Message = entity.Text,
				Title = entity.Type,
			};
		}
	}
}
