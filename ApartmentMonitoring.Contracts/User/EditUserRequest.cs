namespace ApartmentMonitoring.Contracts.User
{
	public class EditUserRequest
	{
		public string Name { get; set; }

		public string Phone { get; set; }
		public string Email { get; set; }

		public string ProfilePhoto { get; set; }
	}
}
