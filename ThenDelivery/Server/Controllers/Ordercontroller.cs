using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using ThenDelivery.Shared.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.Common.Interfaces;
using ThenDelivery.Server.Application.OrderController.Commands;
using ThenDelivery.Server.Application.OrderController.Queries;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Enums;

namespace ThenDelivery.Server.Controllers
{
	public class OrderController : CustomControllerBase<OrderController>
	{
		private readonly ICurrentUserService _currentUserService;

		public OrderController(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
		}

		/// <summary>
		/// This method used for shipper only
		/// </summary>
		/// <param name="orderDto"></param>
		/// <returns></returns>
		[HttpPut]
		[Authorize(Roles = Const.Role.ShipperRole)]
		public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderDto orderDto)
		{
			try
			{
				string shipperId = _currentUserService.GetLoggedInUserId();
				await Mediator.Send(new UpdateOrderStatusCommand(orderDto.Id, orderDto.Status, shipperId));
				return Ok(new CustomResponse(200, "Update success"));
			}
			catch (UpdateOrderStatusException ex)
			{
				return BadRequest(new CustomResponse(ex.StatusCode, "Đơn hàng đã được shipper khác chọn, vui lòng chọn đơn hàng khác"));
			}
			catch (Exception ex)
			{
				return BadRequest(new CustomResponse(400, ex.Message));
			}
		}

		[HttpPost]
		[Authorize(Roles = Const.Role.UserRole)]
		public async Task<IActionResult> AddOrder(OrderDto orderDto)
		{
			try
			{
				orderDto.User = new UserDto() { Id = _currentUserService.GetLoggedInUserId() };
				await Mediator.Send(new InsertOrderCommand(orderDto));

				return Ok(new CustomResponse(200, "Đặt hàng thành công"));
			}
			catch (Exception ex)
			{
				return BadRequest(new CustomResponse(400, ex.Message));
			}
		}

		/// <summary>
		/// Get order list
		/// </summary>
		/// <param name="orderStatus">Enum OrderStatus value [default is OrderSuccess]</param>
		/// <param name="isShipper">Is filter by logged in shipperId [default is false, get all]</param>
		/// <returns>IEnumerable of OrderDto</returns>
		[HttpGet]
		[Authorize(Roles = Const.Role.ShipperRole)]
		public async Task<IActionResult> GetOrderedList(
			byte orderStatus = (byte)OrderStatus.OrderSuccess, bool isShipper = false)
		{
			try
			{
				string shipperId = string.Empty;
				if (isShipper)
				{
					shipperId = _currentUserService.GetLoggedInUserId();
				}
				IEnumerable<OrderDto> orderList =
					await Mediator.Send(new GetAllOrderedQuery((OrderStatus)orderStatus, shipperId));
				return Ok(orderList);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Get order list
		/// </summary>
		/// <param name="orderStatus">Enum OrderStatus value [default is OrderSuccess]</param>
		/// <returns>IEnumerable of OrderDto</returns>
		[HttpGet("user-order-history/{orderStatus}")]
		[Authorize(Roles = Const.Role.UserRole)]
		public async Task<IActionResult> GetOrderedListByUserId(byte orderStatus = (byte)OrderStatus.OrderSuccess)
		{
			try
			{
				string userId = _currentUserService.GetLoggedInUserId();
				IEnumerable<OrderDto> orderList =
					await Mediator.Send(new GetOrderHistoryQuery((OrderStatus)orderStatus, userId));
				return Ok(orderList);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("order-of-merchant")]
		[Authorize(Roles = Const.Role.UserRole)]
		public async Task<IActionResult> GetOrderedListBymerchantId(int merchantId)
		{
			try
			{
				IEnumerable<OrderDto> orderList =
					await Mediator.Send(new GetOrderByMerchantId(merchantId));
				return Ok(orderList);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
