using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.Common.Interfaces;
using ThenDelivery.Server.Application.OrderController.Commands;
using ThenDelivery.Server.Application.OrderController.Queries;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	public class Ordercontroller : CustomControllerBase<Ordercontroller>
	{
		private readonly ICurrentUserService _currentUserService;

		public Ordercontroller(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
		}

		[HttpPost]
		[Authorize(Roles = Const.Role.UserRole)]
		public async Task<IActionResult> AddOrder(OrderDto orderDto)
		{
			try
			{
				orderDto.User.Id = _currentUserService.GetLoggedInUserId();
				await Mediator.Send(new InsertOrderCommand(orderDto));

				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Authorize(Roles = Const.Role.ShipperRole)]
		public async Task<IActionResult> GetOrderedList()
		{
         try
         {
				IEnumerable<OrderDto> orderList = await Mediator.Send(new GetAllOrderedQuery());
				return Ok(orderList);
			}
         catch (Exception ex)
         {
				return BadRequest(ex.Message);
         }
		}
	}
}
