using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.DistrictController.Queries;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	public class DistrictController : CustomControllerBase<DistrictController>
	{
		[HttpGet]
		public async Task<IActionResult> GetAllDistrict()
		{
			IEnumerable<DistrictDto> districts = await Mediator.Send(new GetAllDistrictQuery());
			return Ok(districts.ToList());
		}
	}
}
