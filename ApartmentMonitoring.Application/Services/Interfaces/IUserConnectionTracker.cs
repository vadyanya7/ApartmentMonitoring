namespace ApartmentMonitoring.Application.Services.Interfaces
{
	public interface IUserConnectionTracker
	{
		void AddConnection(long userId, string connectionId);
		void RemoveConnection(string connectionId);
		IReadOnlyCollection<string> GetConnectionIds(long userId);
		IReadOnlyCollection<long> GetOnlineUserIds();
	}
}
