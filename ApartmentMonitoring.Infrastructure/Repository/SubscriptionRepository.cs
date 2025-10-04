using ApartmentMonitoring.Entity.Entities;
using ApartmentMonitoring.Entity.Repository;
using ApartmentMonitoring.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Subscription = ApartmentMonitoring.Entity.Entities.Subscription;

namespace ApartmentMonitoring.Infrastructure.Repository
{
	public class SubscriptionRepository : RepositoryBase, ISubscriptionRepository
	{
		public SubscriptionRepository(DataBaseContext dbContext) : base(dbContext)
		{
		}

		public async Task<ApartmentMonitoring.Entity.Entities.Subscription> AddAsync(ApartmentMonitoring.Entity.Entities.Subscription entity)
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

		public async Task<ApartmentMonitoring.Entity.Entities.Subscription?> GetByIdAsync(long id)
		{
			return await dbContext.Subscriptions.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<List<ApartmentMonitoring.Entity.Entities.Subscription>> GetSubscriptionsByApartment(Apartment apartment)
		{
			var subscriptions = await dbContext.Subscriptions
				.Where(f =>
						   (f.District == null || f.District == apartment.District) &&
						   (f.MinPrice == null || f.MinPrice <= apartment.Price) &&
						   (f.MaxPrice == null || f.MaxPrice >= apartment.Price) &&	
						   (f.MinSize == null || f.MinSize <= apartment.Square) &&
						   (f.MaxSize == null || f.MaxSize >= apartment.Square) &&
						   (f.Floor == null || f.Floor >= apartment.Floor))
				.ToListAsync();

			return subscriptions;
		}

		public async Task<List<ApartmentMonitoring.Entity.Entities.Subscription>> GetSubscriptionsByUser(long userId)
		{
			var result = await dbContext.Subscriptions
						.Include(x => x.User)
						.Where(x => x.User.Id == userId)
						.ToListAsync();

			return result;
		}


		public async Task UpdateAsync(ApartmentMonitoring.Entity.Entities.Subscription entity)
		{
			dbContext.Update(entity);
			await SaveChangesAsync();
		}
	}
}
