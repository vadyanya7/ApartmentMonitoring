using ApartmentMonitoring.Contracts.Subscriptions;

namespace ApartmentMonitoring.Application.Services.Interfaces
{
	public interface ISubscriptionService
	{
		public Task<SubscriptionDto> Add(AddSubscriptionRequest subscriptionRequest);
		public Task<SubscriptionDto> Update(long id, SubscriptionDto subscriptionDto);
		public Task Remove(long id);
		public Task<List<SubscriptionDto>> GetSubscriptions(long userId);
	}
}
