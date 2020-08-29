using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.Common.Interfaces;
using ThenDelivery.Server.Application.MerchantController.Commands;
using ThenDelivery.Server.Application.MerchantController.Queries;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Enums;
using ThenDelivery.Shared.Exceptions;

namespace ThenDelivery.Server.Controllers
{
	[AllowAnonymous]
	public class MerchantController : CustomControllerBase<MerchantController>
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly IImageService _imageService;


		public MerchantController(ICurrentUserService currentUserService,
			IImageService imageService)
		{
			_currentUserService = currentUserService;
			_imageService = imageService;
		}

		[HttpPost]
		[Authorize(Roles = Const.Role.UserRole + "," + Const.Role.AdministrationRole)]
		public async Task<IActionResult> CreateMerchant([FromBody] MerchantDto merchantDto)
		{
			string pathAvatar = _imageService.SaveImage(merchantDto.Avatar, "Merchant\\Avatar");
			string pathCoverPicture = _imageService.SaveImage(merchantDto.CoverPicture, "Merchant\\CoverPicture");
			merchantDto.Avatar = pathAvatar;
			merchantDto.CoverPicture = pathCoverPicture;
			merchantDto.User = new UserDto() { Id = _currentUserService.GetLoggedInUserId() };
			int createdMerchantId = await Mediator.Send(new InsertMerchantCommand(merchantDto));

			// valid if data insert fail
			if (createdMerchantId == -1)
			{
				return BadRequest();
			}
			return Ok(createdMerchantId);
		}

		[HttpPut]
		[Authorize(Roles = Const.Role.UserRole + "," + Const.Role.AdministrationRole)]
		public async Task<IActionResult> EditMerchant([FromBody] MerchantDto merchantDto)
		{
			try
			{
				string pathAvatar = _imageService.SaveImage(merchantDto.Avatar, "Merchant\\Avatar");
				string pathCoverPicture = _imageService.SaveImage(merchantDto.CoverPicture, "Merchant\\CoverPicture");
				merchantDto.Avatar = pathAvatar;
				merchantDto.CoverPicture = pathCoverPicture;
				merchantDto.User = new UserDto() { Id = _currentUserService.GetLoggedInUserId() };
				await Mediator.Send(new UpdateMerchantCommand(merchantDto));
				return Ok(new CustomResponse(200, "Cập nhật thành công"));
			}
			catch (Exception ex)
			{
				return BadRequest(new CustomResponse(400, ex.Message));
			}
		}

		[HttpPut("close")]
		[Authorize(Roles = Const.Role.MerchantRole + "," + Const.Role.UserRole)]
		public async Task<IActionResult> CloseMerchant([FromBody] MerchantDto merchantDto)
		{
			try
			{
				await Mediator.Send(new UpdateMerchantStatusCommand(merchantDto.Id, MerchantStatus.Closed));
				return Ok(new CustomResponse(200, "Cập nhật thành công"));
			}
			catch (Exception ex)
			{
				return BadRequest(new CustomResponse(400, ex.Message));
			}
		}

		[HttpPut("open")]
		[Authorize(Roles = Const.Role.MerchantRole + "," + Const.Role.UserRole)]
		public async Task<IActionResult> OpenMerchant([FromBody] MerchantDto merchantDto)
		{
			try
			{
				await Mediator.Send(new UpdateMerchantStatusCommand(merchantDto.Id, MerchantStatus.Approved));
				return Ok(new CustomResponse(200, "Cập nhật thành công"));
			}
			catch (Exception ex)
			{
				return BadRequest(new CustomResponse(400, ex.Message));
			}
		}

		[HttpPut("approve")]
		[Authorize(Roles = Const.Role.AdministrationRole)]
		public async Task<IActionResult> ApproveMerchant([FromBody] MerchantDto merchantDto)
		{
			try
			{
				await Mediator.Send(new UpdateMerchantStatusCommand(merchantDto.Id, MerchantStatus.Approved));
				return Ok(new CustomResponse(200, "Cập nhật thành công"));
			}
			catch (Exception ex)
			{
				return BadRequest(new CustomResponse(400, ex.Message));
			}
		}

		[HttpGet("my")]
		[Authorize(Roles = Const.Role.MerchantRole + "," + Const.Role.UserRole)]
		public async Task<IActionResult> GetMerchantCurrentUserId()
		{
			try
			{
				string userId = _currentUserService.GetLoggedInUserId();
				IEnumerable<MerchantDto> merchantList = await Mediator.Send(new GetMerchantsByUserId(userId));
				return Ok(merchantList.ToList());
			}
			catch (Exception ex)
			{
				return BadRequest(new CustomResponse(400, ex.Message));
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetMerchant(int merchantId = -1)
		{
			if (merchantId == -1)
			{
				try
				{
					IEnumerable<MerchantDto> merchantList = await Mediator.Send(new GetAllMerchantQuery());
					return Ok(merchantList.ToList());
				}
				catch (Exception ex)
				{
					return BadRequest(new CustomResponse(400, ex.Message));
				}
			}
			else
			{
				try
				{
					MerchantDto merchant = await Mediator.Send(new GetMerchantByIdQuery(merchantId));
					return Ok(merchant);
				}
				catch (Exception ex)
				{
					return BadRequest(new CustomResponse(400, ex.Message));
				}
			}
		}

		[HttpGet("all")]
		[Authorize(Roles = Const.Role.AdministrationRole)]
		public async Task<IActionResult> GetAllMerchant()
		{
			try
			{
				IEnumerable<MerchantDto> merchantList =
					await Mediator.Send(new GetAllMerchantQuery(MerchantStatus.None));
				return Ok(merchantList);
			}
			catch (Exception ex)
			{
				return BadRequest(new CustomResponse(400, ex.Message));
			}
		}

		[HttpGet("isExist")]
		[Authorize(Roles = Const.Role.MerchantRole + "," + Const.Role.UserRole)]
		public async Task<IActionResult> IsExistMerchant(int merchantId)
		{
			try
			{
				var userId = _currentUserService.GetLoggedInUserId();
				bool result = await Mediator.Send(new CheckExistMerchantQuery(merchantId, userId));
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(new CustomResponse(400, ex.Message));
			}
		}
	}
}
