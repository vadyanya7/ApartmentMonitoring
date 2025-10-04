namespace ApartmentMonitoring.Contracts.Banner
{
	public class BannerDto
	{
		public long Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string ImageUrl { get; set; } = string.Empty;
		public string RedirectUrl { get; set; } = string.Empty;
		public int Order { get; set; }
	}
}
