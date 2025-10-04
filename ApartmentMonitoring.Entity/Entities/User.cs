using ApartmentMonitoring.Entity.Enums;

namespace ApartmentMonitoring.Entity.Entities
{
	public class User
	{
		public long Id { get; set; }
		public string Name { get; set; }

		public string Phone { get; set; }
		public string Email { get; set; }

		public string PasswordHash { get; set; }

		public string ProfilePhoto {  get; set; }
		public string InviteCode {  get; set; }

		public long InvitedBy { get; set; }
		public ICollection<User> InvitedUsers { get; set; } = new List<User>();

		public ICollection<Apartment> Apartments { get; set; }

		public ICollection<Subscription> Subscriptions { get; set; }
		public ICollection<InviteCode> InviteCodes { get; set; }

		public ICollection<DailyUserActivity> Activities { get; set; } = new List<DailyUserActivity>();

		public ushort Credit = 100;

		public DateTime CreatedAt {  get; set; }

		public UserRole Role { get; set; } = UserRole.User;
		public bool IsBlocked { get; set; } = false; 
		public DateTime? BlockedUntil { get; set; }
	}
}
