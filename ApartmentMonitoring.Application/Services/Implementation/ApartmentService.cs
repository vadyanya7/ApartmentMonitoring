using ApartmentMonitoring.Application.Mapping;
using ApartmentMonitoring.Application.Services.Interfaces;
using ApartmentMonitoring.Contracts.Apartments;
using ApartmentMonitoring.Entity.Entities;
using ApartmentMonitoring.Entity.Repository;


namespace ApartmentMonitoring.Application.Services.Implementation
{
	public class ApartmentService : IApartmentService
	{
		private readonly IApartmentRepository _apartmentRepository;
		private readonly IUserRepository _userRepository;

		public ApartmentService(IApartmentRepository apartmentRepository,
			IUserRepository userRepository)
		{
			this._apartmentRepository = apartmentRepository;
			this._userRepository = userRepository;
		}

		public async Task<ApartmentDto> Add(string userEmail, ApartmentDto apartment)
		{
			var entity = apartment.ToEntity();
			var user = await _userRepository.GetByEmail(userEmail);
			entity.User = user;
			entity.CreatedAt = DateTime.UtcNow;
			await _apartmentRepository.Add(entity);
			return apartment;
		}

		public async Task Remove(long id)
		{
			 await _apartmentRepository.Remove(id);
		}

		public async Task<ApartmentDto> GetApartment(long id)
		{
			var apartment = await _apartmentRepository.GetApartment(id);
			if (apartment == null)
			{
				throw new Exception("Not Found!");
			}
			apartment.ViewedCount++;
			await _apartmentRepository.Update(apartment);

			return apartment.ToDto();
		}

		public async Task<List<ApartmentDto>> GetApartments(GetApartmentsRequest getApartmentsRequest)
		{
			var subscription = new Subscription();
			subscription.District = getApartmentsRequest.ApartmentFilter.District;
			subscription.Rooms = getApartmentsRequest.ApartmentFilter.Rooms;
			subscription.MaxPrice = getApartmentsRequest.ApartmentFilter.MaxPrice;
			subscription.MinPrice = getApartmentsRequest.ApartmentFilter.MinPrice;
			subscription.MinSize = getApartmentsRequest.ApartmentFilter.MinSize;
			subscription.MaxSize = getApartmentsRequest.ApartmentFilter.MaxSize;
			
			var apartments = await _apartmentRepository.GetApartmentsByFilter(subscription, getApartmentsRequest.LastId, getApartmentsRequest.Count);
			if (!apartments.Any())
			{
				throw new Exception("Not Found!");
			}
			var result = apartments.Select(x=>x.ToDto()).ToList();
			
			return result;
		}

		public async Task<List<ApartmentDto>> GetApartmentsByUser(long userId)
		{
			var apartments =  await _apartmentRepository.GetApartmentsByUser(userId);

			if (!apartments.Any())
			{
				throw new Exception("Not Found!");
			}
			var result = apartments.Select(x=>x.ToDto()).ToList();
			return result;
		}

		public async Task<ApartmentDto> Update(long id, ApartmentDto apartment)
		{
			var oldAprtment = await _apartmentRepository.GetApartment(id);

			oldAprtment.Description = apartment.Description;
			oldAprtment.Price = apartment.Price;
			oldAprtment.Address = apartment.Address;
			oldAprtment.Rooms = apartment.Rooms;
			oldAprtment.UrlsLinks = apartment.UrlsLinks;

			var appartment =  await _apartmentRepository.Update(oldAprtment);

			return appartment.ToDto();
		}
	}
}
