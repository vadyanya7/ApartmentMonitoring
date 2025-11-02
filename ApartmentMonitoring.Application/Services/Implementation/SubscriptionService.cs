//using ApartmentMonitoring.Application.Mapping;
//using ApartmentMonitoring.Application.Services.Interfaces;
//using ApartmentMonitoring.Contracts.Subscriptions;
//using ApartmentMonitoring.Entity.Entities;
//using ApartmentMonitoring.Entity.Repository;


//namespace ApartmentMonitoring.Application.Services.Implementation
//{
//	public class SubscriptionService : ISubscriptionService
//	{
//		private readonly ISubscriptionRepository _repository;
//		private readonly IUserRepository _userRepository;
//		public SubscriptionService(
//			ISubscriptionRepository repository, 
//			IUserRepository _userRepository)
//		{
//			 this._repository = repository;
//			this._userRepository = _userRepository;
//		}

//		public async Task<SubscriptionDto> Add(AddSubscriptionRequest subscriptionRequest)
//		{
//			var user = await _userRepository.GetByEmail(subscriptionRequest.UserEmail);
//			if (user.Subscriptions.Count <= 3)
//			{
//				var subscription = new Subscription()
//				{
//					District = subscriptionRequest.District,
//					Floor = subscriptionRequest.Floor,
//					MaxPrice = subscriptionRequest.MaxPrice,
//					MinPrice = subscriptionRequest.MinPrice,
//					Rooms = subscriptionRequest.Rooms,
//					MinSize = subscriptionRequest.MinSize,
//					MaxSize = subscriptionRequest.MaxSize,
//					User = user,

//				};
//				await _repository.AddAsync(subscription);
//				return subscription.ToDto();
//			}

//			throw new Exception("Count of subscription more than 3 for user.");
			
//		}

//		public async Task<List<SubscriptionDto>> GetSubscriptions(long userId)
//		{
//			var result = await _repository.GetSubscriptionsByUser(userId);
		
//			if (!result.Any())
//			{
//				throw new Exception("Not Found!");
//			}
//			return result.Select(x=>x.ToDto()).ToList();
//		}

//		public async Task Remove(long id)
//		{
//			var subscription = await _repository.GetByIdAsync(id);
//			if (subscription != null)
//			{
//				await _repository.DeleteAsync(subscription);
//			}
//		}

//		public async Task<SubscriptionDto> Update(long id, SubscriptionDto subscriptioDto)
//		{
//			var oldSubscription = await _repository.GetByIdAsync(id);

//			if (subscriptioDto != null)
//			{
//				oldSubscription.MinSize = subscriptioDto.MinSize;
//				oldSubscription.MaxSize = subscriptioDto.MaxSize;
				
//				oldSubscription.MinPrice = subscriptioDto.MinPrice;
//				oldSubscription.MaxPrice = subscriptioDto.MaxPrice;
//				oldSubscription.Floor = subscriptioDto.Floor;
//				oldSubscription.District = subscriptioDto.District;
//				oldSubscription.Rooms = subscriptioDto.Rooms;
				
//				// ADD FIELDS

//				await _repository.UpdateAsync(oldSubscription);
//			}
//			return oldSubscription.ToDto();
//		}
//	}
//}
