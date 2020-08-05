using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.MerchantMenuController.Commands;
using ThenDelivery.Server.Application.MerchantMenuController.Queries;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	[AllowAnonymous]
	public class MenuItemController : CustomControllerBase<MenuItemController>
	{
		[HttpPost]
		[Authorize(Roles = Const.Role.MerchantRole)]
		public async Task<IActionResult> AddRangeMenuItem(IEnumerable<MenuItemDto> menuList)
		{
			try
			{
				await Mediator.Send(new InsertRangeMenuItemCommand(menuList));
				return Ok("Insert merchant menu success");
			}
			catch (ArgumentNullException)
			{
				return BadRequest("MenuItemList is null");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetMenuItemsByMerchantId(int merchantId)
		{
			try
			{
				IEnumerable<MenuItemDto> merchantMenues =
						await Mediator.Send(new GetMenuItemsByMerchantIdQuery(merchantId));

				return Ok(merchantMenues.ToList());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
