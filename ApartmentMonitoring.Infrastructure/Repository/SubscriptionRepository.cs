using ApartmentMonitoring.Entity.Entities;
using ApartmentMonitoring.Entity.Repository;
using ApartmentMonitoring.Infrastructure.Context;
using ApartmentMonitoring.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Subscription = ApartmentMonitoring.Entity.Entities.Subscription;

namespace ApartmentMonitoring.Infrastructure.Repository
{
	public class SubscriptionRepository : RepositoryBase, ISubscriptionRepository
	{
		public SubscriptionRepository(SupabaseContext dbContext) : base(dbContext)
		{
		}

		public async Task<Subscription> AddAsync(Subscription entity)
		{
			await dbContext.AddAsync(entity);
			await SaveChangesAsync();
			return entity;
		}

		public async Task DeleteAsync(ApartmentMonitoring.Entity.Entities.Subscription subscription)
		{
			dbContext.Remove(subscription);
			await SaveChangesAsync();
		}

		public Task DeleteAsync(Subscription entity)
		{
			throw new NotImplementedException();
		}

		public async Task<Subscription?> GetByIdAsync(long id)
		{
			return await dbContext.Subscriptions.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<Subscription>> GetSubscriptionsByApartment(Listing apartment)
		{
			var subscriptions = await dbContext.Subscriptions
				//.Where(f =>
				//		   (f.District == null || f.District == apartment.District) &&
				//		   (f.MinPrice == null || f.MinPrice <= apartment.Price) &&
				//		   (f.MaxPrice == null || f.MaxPrice >= apartment.Price) &&	
				//		   (f.MinSize == null || f.MinSize <= apartment.Square) &&
				//		   (f.MaxSize == null || f.MaxSize >= apartment.Square) &&
				//		   (f.Floor == null || f.Floor >= apartment.Floor))
				.ToListAsync();

			return subscriptions;
		}

		public async Task<List<Subscription>> GetSubscriptionsByUser(long userId)
		{
			var result = await dbContext.Subscriptions
						//.Include(x => x.User)
						//.Where(x => x.User.Id == userId)
						.ToListAsync();

			return result;
		}


		public async Task UpdateAsync(Subscription entity)
		{
			dbContext.Update(entity);
			await SaveChangesAsync();
		}
	}
}
