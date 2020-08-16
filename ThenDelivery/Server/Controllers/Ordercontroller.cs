using Microsoft.AspNetCore.Authorization;
using ThenDelivery.Shared.Common;

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
