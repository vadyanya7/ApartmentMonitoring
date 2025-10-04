using ApartmentMonitoring.Entity.Entities;

namespace ApartmentMonitoring.Entity.Repository
{
	public interface INotificationRepository
	{
		Task<Notification> Add(Notification notification);
		Task AddRange(List<Notification> notifications);

		Task<List<Notification>> GetNotificationsByUser(long userId);

		Task<List<Notification>> GetPendingsNotifications(long userId);

		Task<int> SaveChangesAsync();
	}
}
