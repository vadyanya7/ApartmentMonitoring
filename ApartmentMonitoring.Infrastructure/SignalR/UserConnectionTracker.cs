using ApartmentMonitoring.Application.Services.Interfaces;
using System.Collections.Concurrent;

namespace ApartmentMonitoring.Infrastructure.SignalR
{
	public class UserConnectionTracker : IUserConnectionTracker
	{
		private readonly ConcurrentDictionary<long, HashSet<string>> _connectionsByUser = new();
		private readonly ConcurrentDictionary<string, long> _userByConnection = new();

		private readonly object _lock = new();

		public void AddConnection(long userId, string connectionId)
		{
			lock (_lock)
			{
				if (!_connectionsByUser.ContainsKey(userId))
					_connectionsByUser[userId] = new HashSet<string>();

				_connectionsByUser[userId].Add(connectionId);
				_userByConnection[connectionId] = userId;
			}
		}

		public void RemoveConnection(string connectionId)
		{
			lock (_lock)
			{
				if (_userByConnection.TryGetValue(connectionId, out var userId))
				{
					if (_connectionsByUser.TryGetValue(userId, out var connections))
					{
						connections.Remove(connectionId);
						if (connections.Count == 0)
							_connectionsByUser.TryRemove(userId, out _);
					}
					_userByConnection.TryRemove(connectionId, out _);
				}
			}
		}

		public IReadOnlyCollection<string> GetConnectionIds(long userId)
		{
			if (_connectionsByUser.TryGetValue(userId, out var connections))
				return connections.ToList();

			return Array.Empty<string>();
		}
		public IReadOnlyCollection<long> GetOnlineUserIds()
		{
			lock (_lock)
			{
				return _connectionsByUser
					.Where(kvp => kvp.Value.Count > 0)
					.Select(kvp => kvp.Key)
					.ToList();
			}
		}
	}
}
