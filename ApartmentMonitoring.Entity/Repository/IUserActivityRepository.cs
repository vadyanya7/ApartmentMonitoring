using ApartmentMonitoring.Entity.Entities;

namespace ApartmentMonitoring.Entity.Repository
{
	public interface IUserActivityRepository
	{
		Task<DailyUserActivity> LogActivity(DailyUserActivity activity);
		Task<int> GetActivityCount(DateTime date);
		Task<int> GetActivityCount(DateTime from, DateTime to);
		Task<bool> IsActivityExist(long userId, DateTime time);
	}
}
