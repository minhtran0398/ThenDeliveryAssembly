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
	public class MenuItemController : CustomControllerBase<MenuItemController>
	{
		[HttpPost]
		public async Task<IActionResult> AddMerchantMenuList(IEnumerable<MenuItemDto> menuList)
		{
			try
			{
				await Mediator.Send(new InsertRangeMenuItemCommand(menuList));
				return Ok("Insert merchant menu success");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetMerchantMenuByMerchantId(int merchantId)
		{
			IEnumerable<MenuItemDto> merchantMenues = 
				await Mediator.Send(new GetMenuItemsByMerchantIdQuery(merchantId));

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
