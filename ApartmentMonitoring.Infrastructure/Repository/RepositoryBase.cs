using ApartmentMonitoring.Infrastructure.Context;

namespace ApartmentMonitoring.Infrastructure.Repository
{
	public abstract class RepositoryBase
	{
		protected readonly DataBaseContext dbContext;

		public RepositoryBase(DataBaseContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<int> SaveChangesAsync()
		{
			return await dbContext.SaveChangesAsync();
		}
	}
}
