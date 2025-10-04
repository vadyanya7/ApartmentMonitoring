using ApartmentMonitoring.Contracts.Banner;

namespace ApartmentMonitoring.Application.Services.Interfaces
{
	public interface IAdminService
	{
		public Task BlockUser(string email, TimeSpan? duration = null);
		public Task UnBlockUser(string email);
		public Task<List<BannerDto>> GetActiveBanners();
		public Task<BannerDto> AddBanner(CreateBannerRequest request);
		public Task DisableBanner(long id);

	}
}
