using ApartmentMonitoring.Infrastructure.Context;
using ApartmentMonitoring.Infrastructure.DbContexts;

namespace ApartmentMonitoring.Infrastructure.Repository
{
	public abstract class RepositoryBase
	{
		protected readonly SupabaseContext dbContext;

		public RepositoryBase(SupabaseContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<int> SaveChangesAsync()
		{
			return await dbContext.SaveChangesAsync();
		}
	}
}
