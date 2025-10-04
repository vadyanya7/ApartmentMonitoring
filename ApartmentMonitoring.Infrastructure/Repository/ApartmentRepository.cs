using ApartmentMonitoring.Entity.Repository;
using ApartmentMonitoring.Infrastructure.Context;
using ApartmentMonitoring.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApartmentMonitoring.Infrastructure.Repository
{
	public class ApartmentRepository : RepositoryBase, IApartmentRepository
	{
		public ApartmentRepository(DataBaseContext dbContext) : base(dbContext)
		{
		}

		public async Task<Apartment> Add(Apartment apartment)
		{
			apartment.Source = Entity.Enums.Source.Our;
			await dbContext.AddAsync(apartment);
			await SaveChangesAsync();
			return apartment;
		}

		public async Task Remove(long id)
		{
			var entity = await GetApartment(id);
			if (entity != null)
			{
				dbContext.Remove(entity);
			}
		}

		public async Task<List<Apartment>> GetAllApartments()
		{
			return await dbContext.Apartments
				.Include(x=>x.User)
				.ToListAsync();
		}

		public async Task<List<Apartment>> GetApartmentsByUser(long userId)
		{
			var result = await dbContext.Apartments
				.Include(x => x.User)
				.Where(x => x.User.Id == userId)
				.ToListAsync();
			
			return result;
		}

		public async Task<Apartment> GetApartment(long attachmentId)
		{
			return await dbContext.Apartments.FirstOrDefaultAsync(x => x.Id == attachmentId);
		}

		public Task RemoveApartment(long userId, Apartment apartment)
		{
			dbContext.Remove(apartment);

			return Task.CompletedTask;
		}

		public async Task<Apartment> Update(Apartment apartment)
		{
			dbContext.Update(apartment);
			await SaveChangesAsync();

			return  apartment;
		}

		public async Task<List<Apartment>> GetApartmentsByFilter(ApartmentMonitoring.Entity.Entities.Subscription subscription, long? lastId, int count)
		{
			var query = dbContext.Apartments.AsQueryable();

			if (subscription.MinPrice.HasValue)
				query = query.Where(a => a.Price >= subscription.MinPrice.Value);

			if (subscription.MaxPrice.HasValue)
				query = query.Where(a => a.Price <= subscription.MaxPrice.Value);

			if (subscription.MinSize.HasValue)
				query = query.Where(a => a.Square >= subscription.MinSize);

			if (subscription.MaxSize.HasValue)
				query = query.Where(a => a.Square <= subscription.MaxSize);

			if (subscription.Floor > 0)
				query = query.Where(a => a.Floor == subscription.Floor);

			if (!string.IsNullOrWhiteSpace(subscription.District))
				query = query.Where(a => a.District.ToLower().Contains(subscription.District.ToLower()));	  // херово и просто

			if (subscription.Rooms > 0)
				query = query.Where(a => a.Rooms == subscription.Rooms);

			return await query
					.Where(a => !lastId.HasValue || a.Id > lastId.Value)
					.OrderBy(d => d.Id)
					.Take(count)
					.ToListAsync();
		}

		public Task<List<Apartment>> GetApartmentsCreatedAfterAsync(DateTime timestamp)
		{
			return dbContext.Apartments
					.Where(a => a.CreatedAt > timestamp)
					.ToListAsync();
		}
	}
}
