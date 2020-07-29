using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.MerchantMenu.Queries;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	public class MerchantMenuController : CustomControllerBase<MerchantMenuController>
	{
		[HttpGet]
		public async Task<IActionResult> GetMerchantMenuByMerchantId(int merchantId)
		{
			IEnumerable<MerchantMenuDto> merchantMenues = 
				await Mediator.Send(new GetMerchantMenuByMerchantIdQuery(merchantId));

			// valid if data returned null
			if (merchantMenues == null)
			{
				Logger.LogError("Merchant menu returned null");
				return BadRequest();
			}

			return Ok(merchantMenues.ToList());
		}
	}
}
