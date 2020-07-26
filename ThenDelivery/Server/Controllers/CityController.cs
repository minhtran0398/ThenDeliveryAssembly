using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

			// valid if data returned null
			if (cities == null)
			{
				Logger.LogError("City returned null");
				return BadRequest();
			}

			return Ok(cities.ToList());
		}
	}
}
