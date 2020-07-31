using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.MerchantMenuController.Commands;
using ThenDelivery.Server.Application.MerchantMenuController.Queries;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	public class MerchantMenuController : CustomControllerBase<MerchantMenuController>
	{
		[HttpPost]
		public async Task<IActionResult> AddMerchantMenuList(IEnumerable<MerchantMenuDto> menuList)
		{
			try
			{
				await Mediator.Send(new InsertRangeMerchantMenuCommand(menuList));
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

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
