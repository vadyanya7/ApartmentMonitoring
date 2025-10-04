namespace ApartmentMonitoring.Application
{
	public interface IHubNotifier
	{
		Task NotifyUserAsync(long userId, string message);
	}
}
