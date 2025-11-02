namespace ApartmentMonitoring.Contracts.Notifications
{
	public class NotificationDto
	{
		public Guid NotificationId { get; set; }
		public Guid UserId { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
		public DateTime? SentAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public bool IsRead { get; set; } = false;
	}
}
