using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.CityController.Queries;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	public class CityController : CustomControllerBase<CityController>
	{
		[HttpGet]
		public async Task<IActionResult> GetAllCity()
		{
			IEnumerable<CityDto> cities = await Mediator.Send(new GetAllCityQuery());
			return Ok(cities.ToList());
		}
	}
}
