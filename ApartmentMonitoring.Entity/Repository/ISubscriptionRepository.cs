using ApartmentMonitoring.Infrastructure;

namespace ApartmentMonitoring.Entity.Repository
{
	public interface ISubscriptionRepository : IGenericRepository<Subscription>
	{
		Task<List<Subscription>> GetSubscriptionsByUser(long userId);
		Task<List<Subscription>> GetSubscriptionsByApartment(Listing apartment);
	}
}
