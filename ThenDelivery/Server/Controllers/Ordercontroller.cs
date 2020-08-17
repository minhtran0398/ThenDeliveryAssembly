using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.Common.Interfaces;
using ThenDelivery.Server.Application.OrderController.Commands;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	[Authorize(Roles = Const.Role.UserRole)]
	public class Ordercontroller : CustomControllerBase<Ordercontroller>
	{
      private readonly ICurrentUserService _currentUserService;

      public Ordercontroller(ICurrentUserService currentUserService)
      {
         _currentUserService = currentUserService;
      }

      [HttpPost]
      public async Task<IActionResult> AddOrder(OrderDto orderDto)
      {
         try
         {
            orderDto.UserId = _currentUserService.GetLoggedInUserId();
            await Mediator.Send(new InsertOrderCommand(orderDto));

            return Ok();
         }
         catch (Exception ex)
         {
            return BadRequest(ex.Message);
         }
      }
   }
}
