using ApartmentMonitoring.Application.Mapping;
using ApartmentMonitoring.Application.Services.Interfaces;
using ApartmentMonitoring.Contracts.Banner;
using ApartmentMonitoring.Entity.Entities;
using ApartmentMonitoring.Entity.Repository;


namespace ApartmentMonitoring.Application.Services.Implementation
{
	public class AdminService : IAdminService
	{
		private readonly IUserRepository _userRepository;
		private readonly IBannerRepository _bannerRepository;
		public AdminService(IUserRepository userRepository, IBannerRepository bannerRepository) 
		{
			 this._userRepository = userRepository;
			 this._bannerRepository = bannerRepository;
		}

		public async Task<BannerDto> AddBanner(CreateBannerRequest request)
		{
			var banner = new Banner
			{
				Title = request.Title,
				ImageUrl = request.ImageUrl,
				RedirectUrl = request.RedirectUrl,
				Order = request.Order,
				IsActive = true
			};
			await _bannerRepository.Add(banner);

			return banner.ToDto();
		}

		public async Task BlockUser(string email, TimeSpan? duration = null)
		{
			var user = await _userRepository.GetByEmail(email);
			if (user == null)
			{
				throw new Exception("User not found");
			}

			user.IsBlocked = true;
			user.BlockedUntil = duration.HasValue ? DateTime.UtcNow.Add(duration.Value) : null;

			await _userRepository.Update(user);
		}

		public async Task DisableBanner(long id)
		{
			var banner = await _bannerRepository.GetById(id);
			if (banner != null)
			{
				banner.IsActive = false;
				await _bannerRepository.Update(banner);
			}
		}

		public async Task<List<BannerDto>> GetActiveBanners()
		{
			var result = await _bannerRepository.GetActiveBanners();
			
			return result.Select(x => x.ToDto()).ToList();
		}

		public async Task UnBlockUser(string email)
		{
			var user = await _userRepository.GetByEmail(email);
			if (user == null)
			{
				throw new Exception("User not found");
			}

			user.IsBlocked = false;
			user.BlockedUntil = null;
			
			await _userRepository.Update(user);
		}
	}
}
