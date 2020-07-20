using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.UserController.Commands;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	[Authorize(Roles = Const.Role.AdministrationRole)]
	public class UserController : CustomControllerBase<UserController>
	{
		[HttpGet]
		public async Task<IActionResult> GetAllUser()
		{
			IEnumerable<UserDto> users = await Mediator.Send(new GetAllUserQuery());
			return Ok(users.ToList());
		}
	}
}