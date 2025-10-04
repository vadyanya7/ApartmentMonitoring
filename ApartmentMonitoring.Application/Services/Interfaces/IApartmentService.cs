using ApartmentMonitoring.Contracts.Apartments;

namespace ApartmentMonitoring.Application.Services.Interfaces
{
	public interface IApartmentService
	{
		public Task<ApartmentDto> Add(string userEmail, ApartmentDto apartmentDto);
		public Task<ApartmentDto> Update(long id, ApartmentDto apartmentDto);
		public Task Remove(long id);
		public Task<List<ApartmentDto>> GetApartments(GetApartmentsRequest getApartmentsRequest);
		public Task<List<ApartmentDto>> GetApartmentsByUser(long userId);
		public Task<ApartmentDto> GetApartment(long id);
	}
}
