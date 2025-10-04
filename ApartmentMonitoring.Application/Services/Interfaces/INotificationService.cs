using ApartmentMonitoring.Contracts.Notifications;

namespace ApartmentMonitoring.Application.Services.Interfaces
{
	public interface INotificationService
	{
		Task SendNotificationsForNewApartmentsAsync();

		Task<List<NotificationDto>> GetNotificationsByUser(long userId);
	}
}
