//using ApartmentMonitoring.Entity.Repository;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace ApartmentMonitoring.Controllers
//{
//	// Логика чата. Можно удалять. Даже не тестил
//	[ApiController]
//	[Route("api/[controller]")]
//	public class ChatController : ControllerBase
//	{
//		private readonly IChatRepository _chatRepo;
//		private readonly IChatMessageRepository _msgRepo;

//		public ChatController(IChatRepository chatRepo, IChatMessageRepository msgRepo)
//		{
//			_chatRepo = chatRepo;
//			_msgRepo = msgRepo;
//		}

//		[HttpGet("{chatId}/messages")]
//		public async Task<ActionResult> GetMessages(long chatId)
//		{
//			var messages = await _msgRepo.GetMessagesByChatId(chatId);
//			var res = messages.Select(m => new 
//			{
//				Id = m.Id,
//				SenderId = m.SenderId,
//				Body = m.Body,
//				SentAt = m.SentAt,
//				IsRead = m.IsRead
//			}).ToList();

//			return Ok(res);
//		}

//		[HttpGet("user/{userId}/chats")]
//		public async Task<ActionResult> GetChats(long userId)
//		{
//			var chats = await _chatRepo.GetChatsByUserId(userId);

//			var res = chats.Select(c => new 
//			{
//				Id = c.Id,
//				ApartmentId = c.ApartmentId,
//				OtherUserId = c.InitiatorId == userId ? c.ReceiverId : c.InitiatorId,
//				LastMessage = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault()?.Body ?? string.Empty,
//				LastMessageTime = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault()?.SentAt ?? c.CreatedAt
//			}).ToList();

//			return Ok(res);
//		}
//	}

//}
