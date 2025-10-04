
namespace ApartmentMonitoring.Contracts.Banner
{
	public class CreateBannerRequest
	{
		public string Title { get; set; } = null!;
		public string ImageUrl { get; set; } = null!;
		public string? RedirectUrl { get; set; }
		public int Order { get; set; } 
	}
}
