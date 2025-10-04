using ApartmentMonitoring.Entity.Entities;


namespace ApartmentMonitoring.Entity.Repository
{
	public interface IApartmentRepository
	{
		public Task<Apartment> Add(Apartment apartment);
		public Task<Apartment> Update(Apartment apartment);
		public Task Remove(long id);
		Task<Apartment> GetApartment(long attachmentId);

		Task<List<Apartment>> GetAllApartments();
		Task<List<Apartment>> GetApartmentsByFilter(Subscription subscription, long? lastId, int count);
		Task<List<Apartment>> GetApartmentsByUser(long userId);
		Task<List<Apartment>> GetApartmentsCreatedAfterAsync(DateTime timestamp);

	}
}
