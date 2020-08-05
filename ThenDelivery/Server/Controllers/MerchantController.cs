using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.Common.Interfaces;
using ThenDelivery.Server.Application.MerchantController.Commands;
using ThenDelivery.Server.Application.MerchantController.Queries;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	[AllowAnonymous]
	public class MerchantController : CustomControllerBase<MerchantController>
	{
		private readonly ICurrentUserService _currentUserService;

		public MerchantController(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
		}

		[HttpPost]
		[Authorize(Roles = Const.Role.UserRole)]
		public async Task<IActionResult> CreateMerchant([FromBody] MerchantDto merchantDto)
		{
			merchantDto.UserId = _currentUserService.GetLoggedInUserId();
			int createdMerchantId = await Mediator.Send(new InsertMerchantCommand(merchantDto));

			// valid if data insert fail
			if (createdMerchantId == -1)
			{
				return BadRequest();
			}
			return Ok(createdMerchantId);
		}

		[HttpGet]
		public async Task<IActionResult> GetMerchant(int merchantId = -1)
		{
			if(merchantId == -1)
			{
				IEnumerable<MerchantDto> merchantList =
				await Mediator.Send(new GetAllMerchantQuery());

				// valid if data returned null
				if (merchantList == null)
				{
					Logger.LogError("Merchant list returned null");
					return BadRequest();
				}

				return Ok(merchantList.ToList());
			}
			else
			{
				MerchantDto merchant = await Mediator.Send(new GetMerchantByIdQuery(merchantId));

				// valid if data returned null
				if (merchant == null)
				{
					Logger.LogError("Merchant returned null");
					return BadRequest();
				}

				return Ok(merchant);
			}
		}
	}
}
