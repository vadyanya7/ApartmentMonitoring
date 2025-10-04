using ApartmentMonitoring.Entity.Entities;

namespace ApartmentMonitoring.Entity.Repository
{
	public interface IBannerRepository
	{
		public Task<List<Banner>> GetActiveBanners();
		public Task<Banner> Add(Banner user);
		public Task<Banner> Update(Banner user);
		public Task<Banner> GetById(long id);
	}
}
