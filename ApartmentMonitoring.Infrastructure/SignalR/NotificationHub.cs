using ApartmentMonitoring.Application.Services.Interfaces;
using ApartmentMonitoring.Entity.Repository;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ApartmentMonitoring.Infrastructure.SignalR
{
	public class NotificationHub : Hub
	{
		private readonly IUserConnectionTracker _tracker;
		private readonly INotificationRepository _notificationRepository;

		public NotificationHub(IUserConnectionTracker tracker, INotificationRepository notificationRepository)
		{
			_tracker = tracker;
			_notificationRepository = notificationRepository;
		}

		public override async  Task OnConnectedAsync()
		{
			var httpContext = Context.GetHttpContext();
			var userId = httpContext?.Request.Query["userId"];
			if (long.TryParse(userId, out var id))
			{
				_tracker.AddConnection(id, Context.ConnectionId);
				await SendPendingNotifications(id);
			}
			 await  base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception? exception)
		{
			_tracker.RemoveConnection(Context.ConnectionId);
			return base.OnDisconnectedAsync(exception);
		}

		private async Task SendPendingNotifications(long userId)
		{
			var pendingNotifications = await _notificationRepository.GetPendingsNotifications(Guid.NewGuid());
			if (!pendingNotifications.Any()) return;

			//_logger.LogInformation($"Sending {pendingNotifications.Count} pending notifications to user {userId}");

			foreach (var pendingNotification in pendingNotifications)
			{
				try
				{
					await Clients.Caller.SendAsync("ReceiveNotification", pendingNotification);
					pendingNotification.IsRead = true;
					//pendingNotification.SentAt = DateTime.UtcNow;
				}
				catch (Exception ex)
				{
					//_logger.LogError(ex, $"Failed to send pending notification {pendingNotification.Id}");
				}
			}

			await _notificationRepository.SaveChangesAsync();
		}
	}
}
