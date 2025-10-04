using ApartmentMonitoring.Application.Services.Interfaces;
using System.Security.Claims;

namespace ApartmentMonitoring.Middleware
{
	public class UserActivityMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IServiceScopeFactory _scopeFactory;

		public UserActivityMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
		{
			_next = next;
			_scopeFactory = scopeFactory;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			await _next(context);

			if (context.User.Identity.IsAuthenticated)
			{
				var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
				if (!string.IsNullOrEmpty(userId)&& long.TryParse(userId, out long id))
				{
					// Выполняем асинхронно, чтобы не блокировать ответ
					_ = Task.Run(async () =>
					{
						try
						{
							using var scope = _scopeFactory.CreateScope();
							var tracker = scope.ServiceProvider.GetRequiredService<IUserActivityTrackerService>();
							await tracker.TrackUserActivityAsync(id);
						}
						catch (Exception ex)
						{
							// Логируем, но не падаем
							var logger = _scopeFactory.CreateScope().ServiceProvider
								.GetRequiredService<ILogger<UserActivityMiddleware>>();
							logger.LogError(ex, "Failed to track user activity");
						}
					});
				}
			}
		}
	}
}
