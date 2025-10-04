namespace ApartmentMonitoring.Entity.Entities
{
	public class Subscription
	{
		// Id | UserId | MinPrice | MaxPrice | District | MinArea | MaxArea | Rooms
		public long Id { get; set; }
		public User User { get; set; }

		public int? MinPrice { get; set; }

		public int? MaxPrice { get; set; }
		
		public int? MinSize { get; set; }
		public int? MaxSize { get; set; }

		public int? Floor { get; set; }

		public string? District { get; set; }

		public ushort? Rooms { get; set; }

	}
}
