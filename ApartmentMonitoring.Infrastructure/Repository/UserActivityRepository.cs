using ApartmentMonitoring.Entity.Entities;
using ApartmentMonitoring.Entity.Repository;
using ApartmentMonitoring.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ApartmentMonitoring.Infrastructure.Repository
{
	public class UserActivityRepository : RepositoryBase, IUserActivityRepository
	{
		public UserActivityRepository(DataBaseContext dbContext) : base(dbContext)
		{
		}

		public async Task<int> GetActivityCount(DateTime date)
		{
			var count = await dbContext.DailyUserActivities
				.Where(ua => ua.ActivityDate == date.Date)
				.CountAsync();

				return count;
		}

		public async Task<DailyUserActivity> LogActivity(DailyUserActivity activity)
		{
			await dbContext.AddAsync(activity);
			await SaveChangesAsync();
			return activity;
		}


		public async Task<int> GetActivityCount(DateTime from, DateTime to)
		{
			var count = await dbContext.DailyUserActivities
				.Where(ua => ua.ActivityDate >= from && ua.ActivityDate <= to)
				.Select(ua => ua.UserId)
				.Distinct()
				.CountAsync();
			return count;
		}

		public async Task<bool> IsActivityExist(long userId, DateTime time)
		{
			var exist = await dbContext.DailyUserActivities
			.AnyAsync(ua => ua.UserId == userId && ua.ActivityDate == time);
			return exist;
		}
	}
}
