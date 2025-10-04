
namespace ApartmentMonitoring.Contracts.Apartments
{
	public class ApartmentDto
	{
		public int Price { get; set; }
		public string Address { get; set; }
		public int Square { get; set; }
		public string Description { get; set; }

		public string District { get; set; }
		public List<string>? UrlsLinks { get; set; }
		public ushort Rooms { get; set; }
		public ushort Floor { get; set; }
		public string Source { get; set; }
	}
}
