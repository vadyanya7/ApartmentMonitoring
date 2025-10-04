namespace ApartmentMonitoring.Entity.Entities
{
	public class Banner
	{
		public long Id { get; set; }

		public string Title { get; set; } = string.Empty;
		public string ImageUrl { get; set; } = string.Empty;
		public string RedirectUrl { get; set; } = string.Empty;
		public int Order { get; set; } 
		public bool IsActive { get; set; } = true;

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
