using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	[Authorize(Roles = Const.Role.UserRole)]
	public class Ordercontroller : CustomControllerBase<Ordercontroller>
	{
		//[HttpPost]
		//public async Task<IActionResult> AddRangeOrder(IEnumerable<OrderItem> orderItemList)
		//{

		//}
	}
}
