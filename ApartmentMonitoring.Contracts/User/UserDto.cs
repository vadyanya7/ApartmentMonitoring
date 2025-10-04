namespace ApartmentMonitoring.Contracts.User
{
	public class UserDto
	{
		public string Name { get; set; }

		public string Phone { get; set; }
		public string Email { get; set; }

		public string ProfilePhoto { get; set; }

		public ICollection<string> InviteCodes { get; set; }

	}
}
