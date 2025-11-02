using ApartmentMonitoring.Entity.Entities;
using ApartmentMonitoring.Entity.Repository;
using Microsoft.AspNetCore.SignalR;


namespace ApartmentMonitoring.Infrastructure.SignalR
{
	public class ChatHub : Hub
	{
		private readonly IChatRepository _chatRepo;
		private readonly IChatMessageRepository _msgRepo;

		public ChatHub(IChatRepository chatRepo, IChatMessageRepository msgRepo)
		{
			_chatRepo = chatRepo;
			_msgRepo = msgRepo;
		}

		public async Task SendMessage(long chatId, string body)
		{
			var userId = long.Parse(Context.UserIdentifier!);
			var chat = await _chatRepo.GetById(chatId);
			if (chat == null || (chat.InitiatorId != userId && chat.ReceiverId != userId))
				throw new HubException("Access denied");

			var msg = new ChatMessage
			{
				ChatId = chatId,
				SenderId = userId,
				Body = body
			};

			await _msgRepo.Add(msg);

			var targetId = chat.InitiatorId == userId ? chat.ReceiverId : chat.InitiatorId;
			await Clients.User(targetId.ToString()).SendAsync("ReceiveMessage", new
			{
				ChatId = chatId,
				From = userId,
				Body = body,
				SentAt = msg.SentAt
			});
		}
	}
}
