namespace ApartmentMonitoring.Application.Services.Interfaces
{
	public interface IUserActivityTrackerService
	{
		public Task TrackUserActivityAsync(long userId, string activityType = "type");

		public Task<int> GetDAUA(DateTime date);
		
		public Task<int> GetWAU(DateTime date);

		public Task<int> GetMAU(DateTime date);
	}
}
