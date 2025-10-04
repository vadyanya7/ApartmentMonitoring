namespace ApartmentMonitoring.Contracts.Apartments
{
	public class ApartmentFilter
	{
		public int? MinPrice { get; set; }

		public int? MaxPrice { get; set; }

		public int? MinSize { get; set; }
		public int? MaxSize { get; set; }

		public int? Floor { get; set; }

		public string? District { get; set; }

		public ushort? Rooms { get; set; }
	}
}
