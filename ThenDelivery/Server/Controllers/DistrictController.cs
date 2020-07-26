using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
		public async Task<IActionResult> GetAllDistrict(string cityCode = null)
		{
			IEnumerable<DistrictDto> districts;
			if (String.IsNullOrEmpty(cityCode))
			{
				districts = await Mediator.Send(new GetAllDistrictQuery());
			}
			else
			{
				districts = await Mediator.Send(new GetDistrictByCityCodeQuery(cityCode));
			}

			// valid if data returned null
			if(districts == null)
			{
				Logger.LogError("District returned null");
				return BadRequest();
			}
			
			return Ok(districts.ToList());
		}
	}
}
