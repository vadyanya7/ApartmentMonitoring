using ApartmentMonitoring.Entity.Entities;
using ApartmentMonitoring.Entity.Repository;
using ApartmentMonitoring.Infrastructure.Context;
using ApartmentMonitoring.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

//namespace ApartmentMonitoring.Infrastructure.Repository
//{
//	public class BannerRepository : RepositoryBase, IBannerRepository
//	{
//		public BannerRepository(SupabaseContext dbContext) : base(dbContext)
//		{
//		}

//		public async Task<Banner> Add(Banner banner)
//		{
//			await dbContext.AddAsync(banner);
//			await SaveChangesAsync();
//			return banner;
//		}

//		public async Task<List<Banner>> GetActiveBanners()
//		{
//			return await dbContext.Banners
//				.Where(b => b.IsActive)
//				.OrderBy(b => b.Order)
//				.Take(3)
//				.ToListAsync();
//		}

//		public async Task<Banner> GetById(long id)
//		{
//			return await dbContext.Banners
//				.FirstOrDefaultAsync(b => b.Id == id);
//		}

//		public async Task<Banner> Update(Banner user)
//		{
//			dbContext.Update(user);
//			await SaveChangesAsync();

//			return user;
//		}
//	}
//}
