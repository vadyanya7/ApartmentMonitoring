using ApartmentMonitoring.Contracts.Apartments;
using ApartmentMonitoring.Contracts.Notifications;
using ApartmentMonitoring.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				Message = entity.Message,
				Title = entity.Title,
			};
		}
	}
}
