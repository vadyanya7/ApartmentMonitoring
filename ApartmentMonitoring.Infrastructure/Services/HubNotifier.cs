using ApartmentMonitoring.Application;
using ApartmentMonitoring.Application.Services.Interfaces;
using ApartmentMonitoring.Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace ApartmentMonitoring.Infrastructure.Services
{
	public class HubNotifier : IHubNotifier
	{
		private readonly IHubContext<NotificationHub> _hubContext;
		private readonly IUserConnectionTracker _tracker;

		public HubNotifier(IHubContext<NotificationHub> hubContext,
			IUserConnectionTracker tracker)
		{
			_hubContext = hubContext;
			_tracker = tracker;
		}

		public async Task NotifyUserAsync(long userId, string message)
		{
			var connectionIds = _tracker.GetConnectionIds(userId);

			if (connectionIds.Any())
			{
				foreach (var id in connectionIds)
				{
					await _hubContext.Clients.Client(id).SendAsync("ReceiveNotification", message);
				}
				//_logger.LogInformation($"Notification sent to user {userId} via SignalR.");
			}
		}
	}
}
