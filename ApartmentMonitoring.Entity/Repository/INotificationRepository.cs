
using ApartmentMonitoring.Infrastructure;

namespace ApartmentMonitoring.Entity.Repository
{
	public interface INotificationRepository
	{
		Task<Notification> Add(Notification notification);
		Task AddRange(List<Notification> notifications);

		Task<List<Notification>> GetNotificationsByUser(Guid userId);

		Task<List<Notification>> GetPendingsNotifications(Guid userId);

		Task<int> SaveChangesAsync();
	}
}
