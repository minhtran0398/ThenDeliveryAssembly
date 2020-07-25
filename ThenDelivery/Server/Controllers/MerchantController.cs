using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.Common.Interfaces;
using ThenDelivery.Server.Application.MerchantController.Commands;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	[Authorize(Roles = Const.Role.UserRole)]
	public class MerchantController : CustomControllerBase<MerchantController>
	{
		private readonly ICurrentUserService _currentUserService;

		public MerchantController(ICurrentUserService currentUserService)
		{
			_currentUserService = currentUserService;
		}

		[HttpPost]
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
	}
}
