using ApartmentMonitoring.Application.Mapping;
using ApartmentMonitoring.Application.Services.Interfaces;
using ApartmentMonitoring.Contracts.User;
using ApartmentMonitoring.Contracts.User.Login;
using ApartmentMonitoring.Contracts.User.Register;
using ApartmentMonitoring.Entity.Entities;
using ApartmentMonitoring.Entity.Repository;
using Microsoft.AspNetCore.Identity;
using System.Numerics;


namespace ApartmentMonitoring.Application.Services.Implementation
{
	//public class UserService : IUserService
	//{
	//	private readonly IPasswordHasher<User> _passwordHasher;
	//	private readonly IUserRepository _userRepository;
	//	private readonly IApartmentRepository _apartmentRepository;
	//	private readonly IJwtTokenGenerator _jwtTokenGenerator;
	//	public UserService(
	//			IUserRepository userRepository,
	//			IApartmentRepository apartmentRepository,
	//			IJwtTokenGenerator jwtTokenGenerator,
	//			IPasswordHasher<User> passwordHasher
	//		)
	//		{
	//			_apartmentRepository = apartmentRepository;
	//			_userRepository = userRepository;
	//			_jwtTokenGenerator = jwtTokenGenerator;
	//			_passwordHasher = passwordHasher;
	//		}
	//	public async Task<UserDto> GetProfile(string login)
	//	{
	//		var user = await _userRepository.GetByEmail( login );
		
	//		if (user == null)
	//		{
	//			throw new Exception("Not Found!");
	//		}
			
	//		var userDto = user.ToDto();
	//		return userDto;
	//	}

	//	public async Task<UserDto> UpdateProfile(string login, EditUserRequest editUserRequest)
	//	{
	//		var user = await _userRepository.GetByEmail(login);

	//		user.ProfilePhoto = editUserRequest.ProfilePhoto;
	//		user.Phone = editUserRequest.Phone;
	//		user.Name = editUserRequest.Name;
	//		await _userRepository.Update(user);
			
	//		return user.ToDto();

	//	}

	//	public Task Invite(string login, string activeCode)
	//	{
	//		throw new NotImplementedException();
	//	}

	//	public async Task<LoginResult> Login(string login, string password)
	//	{
	//		var user = await _userRepository.GetByEmail(login);
			
	//		if (user == null)
	//		{
	//			throw new UnauthorizedAccessException("Invalid credentials.");
	//		}
	//		if (user.IsBlocked)
	//		{
	//			throw new UnauthorizedAccessException("User is blocked");//Result.Fail("User is blocked");
	//		}

	//		if (user.BlockedUntil.HasValue && user.BlockedUntil > DateTime.UtcNow)
	//		{
	//			throw new UnauthorizedAccessException($"User is temporarily blocked until {user.BlockedUntil}");
	//		}
	//		var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

	//		if (result != PasswordVerificationResult.Success)
	//		{
	//			throw new UnauthorizedAccessException("Invalid credentials.");
	//		}

	//		var token = _jwtTokenGenerator.GenerateToken(user);

	//		return new LoginResult(token, user.Id.ToString());
	//	}

	//	public async Task Resistr(RegisterUserRequest registerUserRequest)
	//	{
	//		var user = await _userRepository.GetByEmail(registerUserRequest.Email);

	//		// Проверка на существование email
	//		if (user is not null)
	//		{
	//			throw new Exception("Пользователь с таким email уже существует");
	//		}

	//		// Поиск пригласившего по invite-коду
	//		var inviter = await _userRepository.GetInviterByCode(registerUserRequest.InviteCode);

	//		if (inviter == null)
	//		{
	//			throw new Exception("Неверный или использованный код приглашения");
	//		}
	//		// Создание нового пользователя
	//		var newUser = new User
	//		{
	//			Id = new Random().Next(1, int.MaxValue),
	//			Email = registerUserRequest.Email,
	//			Name = registerUserRequest.Name,
	//			PasswordHash = _passwordHasher.HashPassword(user, registerUserRequest.Password),
	//			CreatedAt = DateTime.UtcNow,
	//			InviteCodes = new List<InviteCode>(),
	//			InviteCode = registerUserRequest.InviteCode,
	//			InvitedBy = inviter.Id ,
	//			Phone = registerUserRequest.Phone,
	//			ProfilePhoto = registerUserRequest.ProfilePhoto
	//		};

	//		// Генерация 3 инвайт-кодов
	//		for (int i = 0; i < 3; i++)
	//		{
	//			newUser.InviteCodes.Add(new InviteCode
	//			{
	//				Code = Guid.NewGuid().ToString("N").ToUpper()[..6],
	//				IsUsed = false
	//			});
	//		}

	//		// Помечаем использованный код как использованный
	//		var usedCode = inviter.InviteCodes.First(c => c.Code == registerUserRequest.InviteCode);
	//		usedCode.IsUsed = true;
	//		usedCode.UsedByUserId = newUser.Id;
	//		usedCode.UsedAt = DateTime.UtcNow;

	//		await _userRepository.Add(newUser);
	//	}

	//	public async Task<UserStatistics> GetUserStatistics(int id)
	//	{
	//		var apartments = await _apartmentRepository.GetApartmentsByUser(id);
	//		int viewed = apartments.Sum(apartment => apartment.ViewedCount);
	//		int linked = apartments.Sum(apartment => apartment.LinkedCount);
	//		UserStatistics result =  new(viewed, linked);
			
	//		return result;
	//	}
	//}
}
