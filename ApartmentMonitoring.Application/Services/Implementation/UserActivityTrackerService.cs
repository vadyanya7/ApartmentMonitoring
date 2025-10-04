using ApartmentMonitoring.Application.Services.Interfaces;
using ApartmentMonitoring.Entity.Entities;
using ApartmentMonitoring.Entity.Repository;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;


namespace ApartmentMonitoring.Application.Services.Implementation
{
	public class UserActivityTrackerService : IUserActivityTrackerService
	{
		private readonly IMemoryCache _cache;
		//private readonly IDbContext _context;
		private readonly ILogger<UserActivityTrackerService> _logger;
		private readonly IUserActivityRepository _userActivityRepository;

		public UserActivityTrackerService(
			IUserActivityRepository userActivityRepository, 
			IMemoryCache cache)
		{
			this._userActivityRepository = userActivityRepository;
			this._cache = cache;
		}

		public async Task TrackUserActivityAsync(long userId, string activityType = "general")
		{
			var today = DateTime.UtcNow.Date;
			var userDayKey = $"user_tracked:{userId}:{today:yyyy-MM-dd}";

			// Если уже отслеживали этого пользователя сегодня - ничего не делаем
			if (_cache.TryGetValue(userDayKey, out _))
				return;

			// Проверяем в БД, была ли уже активность сегодня
			var exists = await _userActivityRepository.IsActivityExist(userId, today);

			if (!exists)
			{
				// Создаем запись в БД только если её ещё нет
				var log = new DailyUserActivity
				{
					UserId = userId,
					ActivityDate = today,
					Action= activityType
				};
				await _userActivityRepository.LogActivity(log);

				// Инвалидируем кеш метрик
				_cache.Remove($"DAU_{today:yyyy-MM-dd}");
				_cache.Remove($"WAU_{today:yyyy-MM-dd}");
				_cache.Remove($"MAU_{today:yyyy-MM}");
			}

			// Кешируем факт отслеживания до конца дня
			var midnight = today.AddDays(1);
			_cache.Set(userDayKey, true, midnight);
		}

		public async Task<int> GetDAUA(DateTime date)
		{
			var cacheKey = $"DAU_{date:yyyy-MM-dd}";

			// Пробуем из кеша
			if (_cache.TryGetValue(cacheKey, out int cachedDau))
				return cachedDau;

			// Если нет в кеше - идем в БД
			var dau = await _userActivityRepository.GetActivityCount(date.Date);

			// Кешируем результат
			var ttl = date.Date == DateTime.UtcNow.Date
				? TimeSpan.FromMinutes(5)   // Текущий день - кешируем на 5 минут
				: TimeSpan.FromHours(24);   // Прошлые дни - кешируем на сутки

			_cache.Set(cacheKey, dau, ttl);
			return dau;
		}

		public async Task<int> GetWAU(DateTime date)
		{
			var cacheKey = $"WAU_{date:yyyy-MM-dd}";

			if (_cache.TryGetValue(cacheKey, out int cachedWau))
			{
				return cachedWau;
			}

			var weekStart = date.Date.AddDays(-(int)date.DayOfWeek);
			var weekEnd = weekStart.AddDays(6);

			var wau = await _userActivityRepository.GetActivityCount(weekStart, weekEnd);

			var ttl = IsCurrentWeek(date)
				? TimeSpan.FromMinutes(15)  // Текущая неделя
				: TimeSpan.FromHours(12);   // Прошлые недели

			_cache.Set(cacheKey, wau, ttl);
			return wau;
		}

		public async Task<int> GetMAU(DateTime date)
		{
			var cacheKey = $"MAU_{date:yyyy-MM}";

			if (_cache.TryGetValue(cacheKey, out int cachedMau))
				return cachedMau;

			var monthStart = new DateTime(date.Year, date.Month, 1);
			var monthEnd = monthStart.AddMonths(1).AddDays(-1);

			var mau = await _userActivityRepository.GetActivityCount(monthStart, monthEnd);

			var ttl = IsCurrentMonth(date)
				? TimeSpan.FromHours(1)     // Текущий месяц
				: TimeSpan.FromDays(7);     // Прошлые месяцы

			_cache.Set(cacheKey, mau, ttl);
			return mau;
		}

		private bool IsCurrentWeek(DateTime date)
		{
			var now = DateTime.UtcNow.Date;
			var weekStart = now.AddDays(-(int)now.DayOfWeek);
			var weekEnd = weekStart.AddDays(6);
			return date.Date >= weekStart && date.Date <= weekEnd;
		}

		private bool IsCurrentMonth(DateTime date)
		{
			var now = DateTime.UtcNow.Date;
			return date.Year == now.Year && date.Month == now.Month;
		}
	}
}
