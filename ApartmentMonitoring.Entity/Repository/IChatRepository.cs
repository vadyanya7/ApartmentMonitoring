using ApartmentMonitoring.Entity.Entities;


namespace ApartmentMonitoring.Entity.Repository
{
	public interface IChatRepository
	{
		Task<Chat?> GetByApartmentAndUsers(long apartmentId, long initiatorId, long receiverId);
		Task Add(Chat chat);
		Task<Chat?> GetById(long chatId);
		Task<List<Chat>> GetChatsByUserId(long userId);
	}
}
