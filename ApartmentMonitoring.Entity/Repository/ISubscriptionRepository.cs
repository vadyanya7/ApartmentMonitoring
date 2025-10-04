using ApartmentMonitoring.Entity.Entities;

namespace ApartmentMonitoring.Entity.Repository
{
	public interface ISubscriptionRepository : IGenericRepository<Subscription>
	{
		Task<List<Subscription>> GetSubscriptionsByUser(long userId);
		Task<List<Subscription>> GetSubscriptionsByApartment(Apartment apartment);
	}
}
