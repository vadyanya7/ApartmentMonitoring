using ApartmentMonitoring.Contracts.User;
using ApartmentMonitoring.Contracts.User.Login;
using ApartmentMonitoring.Contracts.User.Register;
using System.Net.Sockets;

namespace ApartmentMonitoring.Application.Services.Interfaces
{
	public interface IUserService
	{
		public Task<LoginResult> Login(string login, string password);

		public Task Resistr(RegisterUserRequest registerUserRequest);

		public Task<UserDto> GetProfile(string login);

		public Task<UserDto> UpdateProfile(string login, EditUserRequest editUserRequest);

		public Task Invite(string login, string activeCode);
		public Task<UserStatistics> GetUserStatistics(int id);
	}
}
