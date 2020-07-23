using Microsoft.AspNetCore.Mvc;
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
		public async Task<IActionResult> GetAllWard()
		{
			IEnumerable<WardDto> ward = await Mediator.Send(new GetAllWardQuery());
			return Ok(ward.ToList());
		}
	}
}
