//using ApartmentMonitoring.Entity.Entities;
//using ApartmentMonitoring.Entity.Repository;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ApartmentMonitoring.Application.Services.Implementation
//{
//	public class ChatService
//	{
//		private readonly IChatRepository _chatRepo;
//		private readonly IApartmentRepository _apartmentRepo;

//		public ChatService(IChatRepository chatRepo, IApartmentRepository apartmentRepo)
//		{
//			_chatRepo = chatRepo;
//			_apartmentRepo = apartmentRepo;
//		}

//		public async Task<long> StartChatAsync(long apartmentId, long currentUserId)
//		{
//			var apartment = await _apartmentRepo.GetApartment(apartmentId);
//			if (apartment == null || apartment?.User.Id == currentUserId)
//				throw new InvalidOperationException("Invalid request");

//			var existing = await _chatRepo.GetByApartmentAndUsers(apartmentId, currentUserId, apartment.User.Id);
//			if (existing != null) return existing.Id;

//			var chat = new Chat
//			{
//				ApartmentId = apartmentId,
//				InitiatorId = currentUserId,
//				ReceiverId = apartment.User.Id
//			};

//			await _chatRepo.Add(chat);
//			return chat.Id;
//		}
//	}

//}
