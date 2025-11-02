using ApartmentMonitoring.Entity.Repository;
using ApartmentMonitoring.Infrastructure.Context;
using ApartmentMonitoring.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Notification = ApartmentMonitoring.Entity.Entities.Notification;

namespace ApartmentMonitoring.Infrastructure.Repository
{
	public class NotificationRepository : RepositoryBase, INotificationRepository
	{
		public NotificationRepository(SupabaseContext dbContext) : base(dbContext)
		{
		}

		public async Task<Notification> Add(Notification notification)
		{
			await dbContext.AddAsync(notification);

			return notification;
		}

		public async Task AddRange(List<Notification> notifications)
		{
			await dbContext.AddRangeAsync(notifications);
			await SaveChangesAsync();
		}

		public async Task<List<Notification>> GetNotificationsByUser(Guid userId)
		{
			var notifications = await dbContext.Notifications
				.Where(x=> x.UserId == userId)
				.ToListAsync();

			return notifications;
		}

		public async Task<List<Notification>> GetPendingsNotifications(Guid userId)
		{
			var pendingNotifications = await dbContext.Notifications
				.Where(p => p.UserId == userId && !p.IsRead)
				.OrderBy(p => p.CreatedAt)
				.Take(50) // Ограничиваем количество для производительности
				.ToListAsync();

			return pendingNotifications;
		}
	}
}
