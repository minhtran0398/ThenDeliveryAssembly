using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.WardController.Queries;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	public class WardController : CustomControllerBase<WardController>
	{
		[HttpGet]
		public async Task<IActionResult> GetAllWard(string districtCode = null)
		{
			IEnumerable<WardDto> ward;
			if(String.IsNullOrEmpty(districtCode))
			{
				ward = await Mediator.Send(new GetAllWardQuery());
			}
			else
			{
				ward = await Mediator.Send(new GetWardByDistrictCodeQuery(districtCode));
			}

			// valid if data returned null
			if (ward == null)
			{
				Logger.LogError("Ward returned null");
				return BadRequest();
			}

			return Ok(ward.ToList());
		}
	}
}
