using ApartmentMonitoring.Entity.Entities;
using ApartmentMonitoring.Entity.Repository;
using ApartmentMonitoring.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace ApartmentMonitoring.Infrastructure.Repository
//{
//	public class ChatMessageRepository : RepositoryBase, IChatMessageRepository
//	{
//		public ChatMessageRepository(DataBaseContext dbContext) : base(dbContext)
//		{
//		}

//		public async Task Add(ChatMessage message)
//		{
//			await dbContext.ChatMessages.AddAsync(message);
//			await SaveChangesAsync();
//		}

//		public Task<List<ChatMessage>> GetMessagesByChatId(long chatId) =>
//			dbContext.ChatMessages
//				.Where(m => m.ChatId == chatId)
//				.OrderBy(m => m.SentAt)
//				.ToListAsync();

//		public Task<List<ChatMessage>> GetUnreadMessages(long userId) =>
//			dbContext.ChatMessages
//				.Where(m => !m.IsRead && m.SenderId != userId &&
//							(m.Chat.InitiatorId == userId || m.Chat.ReceiverId == userId))
//				.ToListAsync();

//		public async Task MarkAsRead(long messageId)
//		{
//			var msg = await dbContext.ChatMessages.FindAsync(messageId);
//			if (msg != null)
//			{
//				msg.IsRead = true;
//				await SaveChangesAsync();
//			}
//		}
//	}
//}
