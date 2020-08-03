using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.MerchantTypeController.Queries;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	public class MerchantTypeController : CustomControllerBase<MerchantTypeController>
	{
		[HttpGet]
		public async Task<IActionResult> GetAllMerchantType()
		{
			IEnumerable<MerTypeDto> merchantTypes = await Mediator.Send(new GetAllMerchantTypeQuery());

			// valid if data returned null
			if (merchantTypes == null)
			{
				Logger.LogError("Merchant type returned null");
				return BadRequest();
			}

			return Ok(merchantTypes.ToList());
		}
	}
}
