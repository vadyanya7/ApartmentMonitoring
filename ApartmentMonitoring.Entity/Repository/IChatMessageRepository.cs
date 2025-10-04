using ApartmentMonitoring.Entity.Entities;

namespace ApartmentMonitoring.Entity.Repository
{
	public interface IChatMessageRepository
	{
		Task Add(ChatMessage message);
		Task<List<ChatMessage>> GetMessagesByChatId(long chatId);
		Task<List<ChatMessage>> GetUnreadMessages(long userId);
		Task MarkAsRead(long messageId);
	}
}
