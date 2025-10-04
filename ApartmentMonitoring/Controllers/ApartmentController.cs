using ApartmentMonitoring.Contracts.Apartments;
using Microsoft.AspNetCore.Mvc;
using ApartmentMonitoring.Application.Services.Interfaces;

namespace ApartmentMonitoring.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ApartmentController : ControllerBase
	{
		private readonly IApartmentService service;

		public ApartmentController(IApartmentService service)
		{
			this.service = service;
		}

		/// <summary>
		/// IDI MAx
		/// </summary>
		/// <param name="apartmentDto"></param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApartmentDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[Route("Add")]
		public async Task<ApartmentDto> Add(string email, [FromBody] ApartmentDto apartmentDto)
		{
			var url = Environment.GetEnvironmentVariable("https://rditooqcfgkhknohiyex.supabase.co");
			var key = Environment.GetEnvironmentVariable("sb_publishable_ZDZUkhq8xv07G0ACzDVApA_4Eo46_9C");
			var options = new Supabase.SupabaseOptions
			{
				AutoConnectRealtime = true
			};
			var supabase = new Supabase.Client(url, key, options);
			await supabase.InitializeAsync();

			await service.Add(email, apartmentDto);

			return apartmentDto;
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApartmentDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[Route("Update")]
		public async Task<ApartmentDto> Update(long id, [FromBody] ApartmentDto apartmentDto)
		{
			await service.Update(id, apartmentDto);

			return apartmentDto;
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[Route("Remove")]
		public async Task<IActionResult> Remove(long id)
		{
			await service.Remove(id);

			return Ok();
		}

		/// <summary>
		/// Get apartments by filter
		/// </summary>
		/// <param name="getApartmentsRequest"></param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApartmentDto>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[Route("GetApartments")]
		public async Task<List<ApartmentDto>> GetApartments(GetApartmentsRequest getApartmentsRequest = null)
		{
			return await service.GetApartments(getApartmentsRequest);
		}

		/// <summary>
		/// Get apartment by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApartmentDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[Route("GetApartment")]
		public async Task<ApartmentDto> GetApartment(long id)
		{
			return await service.GetApartment(id);
		}

		/// <summary>
		/// Get all apartments of user
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ApartmentDto>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[Route("user/{id}/GetApartments")]
		public async Task<List<ApartmentDto>> GetApartmentsByUser(long id)
		{
			return await service.GetApartmentsByUser(id);
		}
	}
}
