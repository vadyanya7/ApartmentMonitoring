
namespace ApartmentMonitoring.Entity.Entities
{
	public class Notification
	{
		public Guid Id { get; set; }
		public long UserId { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
		public DateTime? SentAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public bool IsRead { get; set; } = false;
	}
}
