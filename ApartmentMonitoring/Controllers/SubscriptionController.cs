//using ApartmentMonitoring.Application.Services.Interfaces;
//using ApartmentMonitoring.Contracts.Notifications;
//using ApartmentMonitoring.Contracts.Subscriptions;
//using Microsoft.AspNetCore.Mvc;

//namespace ApartmentMonitoring.Controllers
//{
//	[Route("[controller]")]
//	[ApiController]
//	public class SubscriptionController : ControllerBase
//	{
//		private readonly ISubscriptionService _subscriptionService;
//		public SubscriptionController(ISubscriptionService subscriptionService)
//		{
//			this._subscriptionService = subscriptionService;
//		}

//		[HttpGet]
//		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SubscriptionDto>))]
//		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
//		[Route("Subscriptions")]
//		public async Task<List<SubscriptionDto>> Subscriptions(long userId)
//		{
//			var subscriptions = await _subscriptionService.GetSubscriptions(userId);

//			return subscriptions;
//		}

//		[HttpPost]
//		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubscriptionDto))]
//		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
//		[Route("Add")]
//		public async Task<SubscriptionDto> Add(long id, [FromBody] AddSubscriptionRequest subscription)
//		{
//			var result = await _subscriptionService.Add(subscription);

//			return result;
//		}

//		[HttpPut]
//		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubscriptionDto))]
//		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
//		[Route("Update")]
//		public async Task<SubscriptionDto> Update(long id, [FromBody] SubscriptionDto subscriptionDto)
//		{
//			var result = await _subscriptionService.Update(id, subscriptionDto);

//			return result;
//		}

//		[HttpDelete]
//		[Route("Remove")]
//		public async Task<IActionResult> Remove(long id)
//		{
//			await _subscriptionService.Remove(id);

//			return Ok();
//		}
//	}

//}
