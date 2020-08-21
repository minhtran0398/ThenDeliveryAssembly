using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.Common.Interfaces;
using ThenDelivery.Server.Application.ShippingAddressController.Queries;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	[Authorize(Roles = Const.Role.UserRole)]
	public class ShippingAddressController : CustomControllerBase<ShippingAddressDto>
	{
		private readonly ICurrentUserService _currentUserService;

		public ShippingAddressController(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
		}

		[HttpGet]
		public async Task<IActionResult> GetShippingAddressByUserId()
		{
			try
			{
				string userId = _currentUserService.GetLoggedInUserId();

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
