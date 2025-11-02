//using ApartmentMonitoring.Application.Services.Interfaces;
//using ApartmentMonitoring.Contracts.Banner;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace ApartmentMonitoring.Controllers
//{
//	[Authorize(Roles = "Admin")]
//	[Route("api/[controller]")]
//	[ApiController]
//	public class AdminController : ControllerBase
//	{
//		private readonly IUserActivityTrackerService _userActivityTrackerService;
//		private readonly IAdminService _adminService;
//		public AdminController(IUserActivityTrackerService userActivityTrackerService,
//			IAdminService adminService)
//		{
//			this._userActivityTrackerService = userActivityTrackerService;
//			this._adminService = adminService;
//		}

//		[HttpGet]
//		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
//		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
//		[Route("mau")]
//		public async Task<int> Mau(DateTime time)
//		{
//			return await _userActivityTrackerService.GetMAU(time);
//		}

//		[HttpGet]
//		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
//		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
//		[Route("wau")]
//		public async Task<int> Wau(DateTime time)
//		{
//			return await _userActivityTrackerService.GetWAU(time);
//		}

//		[HttpGet]
//		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
//		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
//		[Route("dau")]
//		public async Task<int> Dau(DateTime time)
//		{
//			return await _userActivityTrackerService.GetDAUA(time);
//		}

//		[HttpPost]
//		[Route("Block")]
//		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
//		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
//		public async Task<IActionResult> Block(string email)
//		{
//			await _adminService.BlockUser(email);
//			return Ok();
//		}

//		[HttpPost]
//		[Route("UnBlock")]
//		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
//		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
//		public async Task<IActionResult> UnBlock(string email)
//		{
//			await _adminService.UnBlockUser(email);
//			return Ok();
//		}

//		[AllowAnonymous]
//		[HttpGet]
//		[Route("GetActiveBanners")]
//		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BannerDto))]
//		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
//		public async Task<List<BannerDto>> GetActiveBanners()
//		{
//			var banners = await _adminService.GetActiveBanners();
//			return banners;
//		}

//		[HttpPost]
//		[Route("Create")]
//		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BannerDto))]
//		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
//		public async Task<BannerDto> Create([FromBody] CreateBannerRequest request)
//		{
//			var dto = await _adminService.AddBanner(request);
//			return dto;
//		}

//		[HttpPost("disable/{id}")]
//		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
//		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
//		public async Task<IActionResult> Disable(long id)
//		{
//			await _adminService.DisableBanner(id);
//			return Ok();
//		}
//	}
//}
