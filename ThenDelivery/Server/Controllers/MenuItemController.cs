using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.MenuItemController.Commands;
using ThenDelivery.Server.Application.MerchantController.Queries;
using ThenDelivery.Server.Application.MerchantMenuController.Commands;
using ThenDelivery.Server.Application.MerchantMenuController.Queries;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Exceptions;

namespace ThenDelivery.Server.Controllers
{
	[AllowAnonymous]
	public class MenuItemController : CustomControllerBase<MenuItemController>
	{
		[HttpPost]
		[Authorize(Roles = Const.Role.MerchantRole + "," + Const.Role.UserRole)]
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

		[HttpPut]
		[Authorize(Roles = Const.Role.MerchantRole + "," + Const.Role.UserRole)]
		public async Task<IActionResult> UpdateRangeMenuItem(IEnumerable<MenuItemDto> menuList)
		{
			try
			{
				await Mediator.Send(new UpdateRangeMenuCommand(menuList));
				return Ok(new CustomResponse(200, "Update merchant menu success"));
			}
			catch (ArgumentNullException)
			{
				return BadRequest(new CustomResponse(400, "MenuItemList is null"));
			}
			catch (Exception ex)
			{
				return BadRequest(new CustomResponse(400, ex.Message));
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetMenuItemsByMerchantId(int merchantId)
		{
			try
			{
				IEnumerable<MenuItemDto> merchantMenues =
						await Mediator.Send(new GetMenuItemsByMerchantIdQuery(merchantId));

				return Ok(merchantMenues);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("edit")]
		[Authorize(Roles = Const.Role.MerchantRole + "," + Const.Role.UserRole)]
		public async Task<IActionResult> GetMenuItemForEdit(int merchantId)
      {
         try
         {
				IEnumerable<EditMenuItemVM> merchantMenues =
						await Mediator.Send(new GetEditMenuItemQuery(merchantId));

				return Ok(merchantMenues);
			}
         catch (Exception ex)
         {
				return BadRequest(new CustomResponse(400, ex.Message));
         }
      }

		[HttpPut("edit")]
		[Authorize(Roles = Const.Role.MerchantRole + "," + Const.Role.UserRole)]
		public async Task<IActionResult> UpdateMenuItemMerchant([FromBody] EditMerchantVM editMerchant)
      {
         try
         {
				await Mediator.Send(new UpdateMenuItemCommand(editMerchant));
				return Ok(new CustomResponse(200, "Update merchant menu item menu success"));
			}
         catch (Exception ex)
         {
				return BadRequest(new CustomResponse(400, ex.Message));
         }
      }
	}
}
