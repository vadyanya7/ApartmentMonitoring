using ApartmentMonitoring.Application.Mapping;
using ApartmentMonitoring.Application.Services.Interfaces;
using ApartmentMonitoring.Contracts.Notifications;
using ApartmentMonitoring.Entity.Entities;
using ApartmentMonitoring.Entity.Repository;
using System.Text.Json;

namespace ApartmentMonitoring.Application.Services.Implementation
{
	public class NotificationService : INotificationService
	{
		private readonly IApartmentRepository _apartmentRepository;
		private readonly IUserRepository _userRepository;
		private readonly INotificationRepository _notificationRepo;
		private readonly ISubscriptionRepository _subscriptionRepository;
		private readonly IHubNotifier _hubNotifier;
		private readonly IUserConnectionTracker _userConnectionTracker;

		public NotificationService(
			IApartmentRepository apartmentRepository,
			IUserRepository userRepository,
			INotificationRepository notificationRepo,
			ISubscriptionRepository subscriptionRepository,
			IHubNotifier hubNotifier,
			IUserConnectionTracker userConnectionTracker)
		{
			_apartmentRepository = apartmentRepository;
			_userRepository = userRepository;
			_notificationRepo = notificationRepo;
			_hubNotifier = hubNotifier;
			_subscriptionRepository = subscriptionRepository;
			_userConnectionTracker = userConnectionTracker;
		}

		public async Task<List<NotificationDto>> GetNotificationsByUser(long userId)
		{
			var result = await _notificationRepo.GetNotificationsByUser(userId);
			if (result == null)
			{
				throw new Exception("Not Found!");
			}
			return result.Select(x=>x.ToDto()).ToList();
		}

		public async Task SendNotificationsForNewApartmentsAsync()
		{
			var minutes = -1;
			var newApartments = await _apartmentRepository.GetApartmentsCreatedAfterAsync(DateTime.UtcNow.AddMinutes(minutes));
			if (!newApartments.Any()) return;

			var users = await _userRepository.GetAllWithSubscriptionsAsync();

			foreach (var apartment in newApartments)
			{
				await NotifyMatchingUsers(apartment);
			}

		}
		private async Task NotifyMatchingUsers(Apartment apartment)
		{
			var subscriptions = await _subscriptionRepository.GetSubscriptionsByApartment(apartment);

			if (!subscriptions.Any()) return;

			var userIds = subscriptions.Select(s => s.User.Id).Distinct().ToList();
			var onlineUserIds = _userConnectionTracker.GetOnlineUserIds();

			var offlineUserIds = userIds.Except(onlineUserIds).ToList();

			var msg = $"New apartment: {apartment.Address}, {apartment.Price}$, {apartment.Floor}";

			// Отправляем онлайн пользователям
			foreach (var connection in onlineUserIds)
			{
				try
				{
					await _hubNotifier.NotifyUserAsync(connection, msg); ;
				}
				catch (Exception ex)
				{
					//_logger.LogWarning(ex, $"Failed to send notification to connection {connection.ConnectionId}");
					// Если не удалось отправить, добавляем в очередь
					offlineUserIds.Add(connection);
				}
			}

			// Сохраняем для офлайн пользователей
			var pendingNotifications = offlineUserIds.Select(userId => new Notification
			{
				UserId = userId,
				Message = msg
			}).ToList();

			if (pendingNotifications.Any())
			{
				await _notificationRepo.AddRange(pendingNotifications);
			}
		}
	}
}
