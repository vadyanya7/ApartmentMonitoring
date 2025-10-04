using ApartmentMonitoring.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace ApartmentMonitoring.Infrastructure.BackgroundServices
{
	public class NotificationJob : BackgroundService
	{
		private readonly IServiceScopeFactory _scopeFactory;
		private readonly ILogger<NotificationJob> _logger;

		public NotificationJob(IServiceScopeFactory scopeFactory, ILogger<NotificationJob> logger)
		{
			_scopeFactory = scopeFactory;
			_logger = logger;
		}


		public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

		protected async override Task ExecuteAsync(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				try
				{
					using var scope = _scopeFactory.CreateScope();
					var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

					await notificationService.SendNotificationsForNewApartmentsAsync();

					await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error occurred while sending notifications.");
				}
			}

			_logger.LogInformation("Notification job stopped.");
		}
	}
}
