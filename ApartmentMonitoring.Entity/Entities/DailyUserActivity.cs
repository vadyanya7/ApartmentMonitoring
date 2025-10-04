namespace ApartmentMonitoring.Entity.Entities
{
	public class DailyUserActivity
	{
		public long Id { get; set; }

		public long UserId { get; set; }
		public User User { get; set; } = null!;
		public DateTime ActivityDate { get; set; } 
		public string Action { get; set; }     // "ViewApartment", "Login", "Search", и т.д.

	}
}
