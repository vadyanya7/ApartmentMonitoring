using ApartmentMonitoring.Entity.Repository;
using ApartmentMonitoring.Infrastructure.Context;
using ApartmentMonitoring.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApartmentMonitoring.Infrastructure.Repository
{
	public class UserRepository : RepositoryBase, IUserRepository
	{
		private readonly DataBaseContext dbContext;
		public UserRepository(DataBaseContext dbContext) : base(dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<ApartmentMonitoring.Entity.Entities.User> GetByEmail(string email)
		{
			return await dbContext.Users
				.Include(x=> x.InviteCodes)
				.Include(x=>x.Subscriptions)
				.FirstOrDefaultAsync(x => x.Email == email)!;
		}

		public async Task<ApartmentMonitoring.Entity.Entities.User> GetInviterByCode(string code)
		{
			var inviter = await dbContext.Users
				.Include(u => u.InviteCodes)
				.FirstOrDefaultAsync(u => u.InviteCodes.Any(c => c.Code == code && !c.IsUsed));

			return inviter!;
		}

		public async Task<ApartmentMonitoring.Entity.Entities.User> Add(ApartmentMonitoring.Entity.Entities.User user)
		{
			await dbContext.Users.AddAsync(user);
			await SaveChangesAsync();

			return user;
		} 
		public void ModifySubsription(long userId, ApartmentMonitoring.Entity.Entities.Subscription subscription)
		{
			throw new NotImplementedException();
		}


		public int UpdateCredit(string username, int score)
		{
			throw new NotImplementedException();
		}

		public void Registr(string username, string password, string activeCode)
		{
			throw new NotImplementedException();
		}

		public async Task<ApartmentMonitoring.Entity.Entities.User> Update(ApartmentMonitoring.Entity.Entities.User user)
		{
			dbContext.Update(user);
			await SaveChangesAsync();
			return user;
		}

		public async Task<List<ApartmentMonitoring.Entity.Entities.User>> GetAllWithSubscriptionsAsync()
		{
			var users = await dbContext.Users
				.Include(u => u.Subscriptions)
				.Where(u => u.Subscriptions.Any())
				.ToListAsync();

			return users;
		}
	}
}
