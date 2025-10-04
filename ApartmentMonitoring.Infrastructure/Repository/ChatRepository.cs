using ApartmentMonitoring.Entity.Entities;
using ApartmentMonitoring.Entity.Repository;
using ApartmentMonitoring.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;


namespace ApartmentMonitoring.Infrastructure.Repository
{
	public class ChatRepository : RepositoryBase, IChatRepository
	{

		public ChatRepository(DataBaseContext dbContext) : base(dbContext)
		{
		}

		public Task<Chat?> GetByApartmentAndUsers(long apartmentId, long initiatorId, long receiverId) =>
			dbContext.Chats
				.FirstOrDefaultAsync(c =>
					c.ApartmentId == apartmentId &&
					c.InitiatorId == initiatorId &&
					c.ReceiverId == receiverId);

		public Task<Chat?> GetById(long chatId) =>
			dbContext.Chats.Include(c => c.Messages).FirstOrDefaultAsync(c => c.Id == chatId);

		public Task<List<Chat>> GetChatsByUserId(long userId) =>
			dbContext.Chats
				.Where(c => c.InitiatorId == userId || c.ReceiverId == userId)
				.ToListAsync();

		public async Task Add(Chat chat)
		{
			await dbContext.Chats.AddAsync(chat);
			await SaveChangesAsync();
		}

		public Task SaveChangesAsync() => dbContext.SaveChangesAsync();
	}
}
