using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.ShippingAddress.Queries;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	[Authorize(Roles = Const.Role.UserRole)]
	public class ShippingAddressController : CustomControllerBase<ShippingAddressDto>
	{
		[HttpGet]
		public async Task<IActionResult> GetShippingAddressByUserId(string userId)
		{
			try
			{
				IEnumerable<ShippingAddressDto> result =
					await Mediator.Send(new GetShippingAddressByUserIdQuery(userId));
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
