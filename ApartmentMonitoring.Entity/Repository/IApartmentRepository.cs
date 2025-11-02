using ApartmentMonitoring.Entity.Entities;
using ApartmentMonitoring.Infrastructure;
using Subscription = ApartmentMonitoring.Infrastructure.Subscription;


namespace ApartmentMonitoring.Entity.Repository
{
	public interface IApartmentRepository
	{
		public Task<Listing> Add(Listing listing);
		public Task<Listing> Update(Listing listing);
		public Task Remove(Guid id);
		Task<Listing> GetListing(Guid listingID);

		Task<List<Listing>> GetAllListings();
		Task<List<Listing>> GetListingsByFilter(Subscription subscription, Guid? lastId, int count);
		Task<List<Listing>> GetListingsByUser(Guid userId);
		Task<List<Listing>> GetListingsCreatedAfterAsync(DateTime timestamp);

	}
}
