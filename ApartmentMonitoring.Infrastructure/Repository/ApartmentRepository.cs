using ApartmentMonitoring.Entity.Repository;
using ApartmentMonitoring.Infrastructure.Context;
using ApartmentMonitoring.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using ApartmentMonitoring.Infrastructure.DbContexts;

namespace ApartmentMonitoring.Infrastructure.Repository
{
	public class ApartmentRepository : RepositoryBase, IApartmentRepository
	{
		public ApartmentRepository(SupabaseContext dbContext) : base(dbContext)
		{
		}

		public async Task<Listing> Add(Listing listing)
		{
			//apartment.Source = Entity.Enums.Source.Our;
			await dbContext.AddAsync(listing);
			await SaveChangesAsync();
			return listing;
		}

		public async Task Remove(Guid id)
		{
			var entity = await GetListing(id);
			if (entity != null)
			{
				dbContext.Remove(entity);
			}
		}

		public async Task<List<Listing>> GetAllListings()
		{
			return await dbContext.Listings
				.Include(x=>x.User)
				.ToListAsync();
		}

		public async Task<List<Listing>> GetListingsByUser(Guid userId)
		{
			var result = await dbContext.Listings
				.Include(x => x.User)
				.Where(x => x.User.Id == userId)
				.ToListAsync();
			
			return result;
		}

		public async Task<Listing> GetListing(Guid listingId)
		{
			return await dbContext.Listings.FirstOrDefaultAsync(x => x.Id == listingId);
		}

		public async Task<Listing> Update(Listing listing)
		{
			dbContext.Update(listing);
			await SaveChangesAsync();

			return  listing;
		}

		public async Task<List<Listing>> GetListingsByFilter(Subscription subscription, Guid? lastId, int count)
		{
			var query = dbContext.Listings.AsQueryable();

			//if (subscription.MinPrice.HasValue)
			//	query = query.Where(a => a.Price >= subscription.MinPrice.Value);

			//if (subscription.MaxPrice.HasValue)
			//	query = query.Where(a => a.Price <= subscription.MaxPrice.Value);

			//if (subscription.MinSize.HasValue)
			//	query = query.Where(a => a.Square >= subscription.MinSize);

			//if (subscription.MaxSize.HasValue)
			//	query = query.Where(a => a.Square <= subscription.MaxSize);

			//if (subscription.Floor > 0)
			//	query = query.Where(a => a.Floor == subscription.Floor);

			//if (!string.IsNullOrWhiteSpace(subscription.District))
			//	query = query.Where(a => a.District.ToLower().Contains(subscription.District.ToLower()));	  // херово и просто

			//if (subscription.Rooms > 0)
			//	query = query.Where(a => a.Rooms == subscription.Rooms);

			return await query
					.Where(a => !lastId.HasValue || a.Id > lastId.Value)
					.OrderBy(d => d.Id)
					.Take(count)
					.ToListAsync();
		}

		public Task<List<Listing>> GetListingsCreatedAfterAsync(DateTime timestamp)
		{
			return dbContext.Listings
				//	.Where(a => a.CreatedAt > timestamp)
					.ToListAsync();
		}
	}
}
