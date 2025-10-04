using ApartmentMonitoring.Application.Services.Interfaces;
using ApartmentMonitoring.Contracts.Apartments;
using ApartmentMonitoring.Contracts.Notifications;
using ApartmentMonitoring.Contracts.Subscriptions;
using ApartmentMonitoring.Entity.Repository;

using Microsoft.AspNetCore.Mvc;

namespace ApartmentMonitoring.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(ILogger<NotificationController> logger, INotificationService notificationService)
        {
         //   _logger = logger;
            this._notificationService = notificationService;
        }

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NotificationDto>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[Route("Notifications")]
		public async Task<List<NotificationDto>> Subscriptions(long userId)
		{
			var notifications = await _notificationService.GetNotificationsByUser(userId);

			return notifications;
		}
	}
}
