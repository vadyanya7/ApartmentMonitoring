namespace ApartmentMonitoring.Contracts.Subscriptions
{
	public class SubscriptionDto
	{
		public long UserId { get; set; }

		public int? MinPrice { get; set; }

		public int? MaxPrice { get; set; }

		public int? MinSize { get; set; }
		public int? MaxSize { get; set; }

		public int? Floor { get; set; }

		public string? District { get; set; }

		public ushort? Rooms { get; set; }
	}
}
