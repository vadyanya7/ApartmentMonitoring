
namespace ApartmentMonitoring.Contracts.User.Register
{
	public class RegisterUserRequest
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public string InviteCode { get; set; }

		public string Phone {  get; set; }
		public string ProfilePhoto {  get; set; }
	}
}
