using ApartmentMonitoring.Application.Services.Interfaces;
using ApartmentMonitoring.Contracts.Subscriptions;
using ApartmentMonitoring.Contracts.User;
using ApartmentMonitoring.Contracts.User.Login;
using ApartmentMonitoring.Contracts.User.Register;
using ApartmentMonitoring.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApartmentMonitoring.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService userService;
		private readonly SupabaseContext supabaseContext;
		public UserController(
		//	IUserService userService, 
			SupabaseContext supabaseContext)
		{
			//this.userService = userService;
			this.supabaseContext = supabaseContext;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResult))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[Route("Login")]
		public async Task<LoginResult> Login(string login, string password)
		{
			var result = await userService.Login(login, password);

			return result;
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[Route("Resister")]
		public async Task<IActionResult> Resister(RegisterUserRequest registerUserRequest)
		{
			await userService.Resistr(registerUserRequest);

			return Ok();
		}

		//[Authorize]
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[Route("GetProfile")]
		public async Task<UserDto> GetProfile(string login)
		{
			var result = await userService.GetProfile(login);
			return result;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[Route("GetUserStatistics")]
		public async Task<UserStatistics> GetUserStatistics(int id)
		{
			var result = await userService.GetUserStatistics(id);
			return result;
		}


		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[Route("Edit")]
		public async Task<UserDto> Edit(string login, EditUserRequest editProfileRequest)
		{
			var result = await userService.UpdateProfile(login, editProfileRequest);

			return result;
		}


		[HttpDelete]
		[Route("Logout")]
		public async Task<IActionResult> Logout()
		{
			var Listings =  await supabaseContext.Listings.ToListAsync();
			return Ok(Listings);
		}


	}
}
