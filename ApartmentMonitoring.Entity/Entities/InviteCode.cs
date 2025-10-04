namespace ApartmentMonitoring.Entity.Entities
{
	public class InviteCode
	{
		public long Id { get; set; }
		public string Code { get; set; } = default!;
		public bool IsUsed { get; set; }
		public long UserId { get; set; }
		public User User { get; set; } = null!;

		public long? UsedByUserId { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime? UsedAt { get; set; }
	}
}
