using ApartmentMonitoring.Entity.Entities;

namespace ApartmentMonitoring.Entity.Repository
{
	public interface IUserRepository
	{
		void Registr(string username, string password, string activeCode);

		Task<User> GetByEmail(string email);
		Task<User> Add(User user);
		Task<User> GetInviterByCode(string code);

		Task<User> Update(User user);

		void ModifySubsription(long userId, Subscription subscription);
		int UpdateCredit(string username, int score);

		Task<List<User>> GetAllWithSubscriptionsAsync();
	}
}
