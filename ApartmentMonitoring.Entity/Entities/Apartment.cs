using ApartmentMonitoring.Entity.Enums;

namespace ApartmentMonitoring.Entity.Entities
{
	public class Apartment
	{
		public long Id { get; set; }
	    //	Id | UserId | MinPrice | MaxPrice | District | MinArea | MaxArea | Rooms

		public int Price { get; set; }
		public string Address { get; set; }
		public int Square { get; set; }
		public string Description {  get; set; }

		public string District { get; set; }
		public List<string>? UrlsLinks { get; set; }
		public ushort Rooms { get; set; }
		public ushort Floor { get; set; }
		public Source Source { get; set; } = Source.Our;

		public DateTime CreatedAt { get; set; }

		public int ViewedCount { get; set; }
		public int LinkedCount { get; set; }

		public User? User { get; set; }
		//public ICollection<ApartmentView> Views { get; set; } = new List<ApartmentView>();

	}
}
