using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.FeaturedDishCategoryController.Queries;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	public class FeaturedDishController : CustomControllerBase<FeaturedDishController>
	{
		[HttpGet]
		public async Task<IActionResult> GetAllFeaturedDishCategory()
		{
			IEnumerable<FeaturedDishDto> featuredDuishCategories =
				await Mediator.Send(new GetAllFeaturedDishQuery());

			// valid if data returned null
			if (featuredDuishCategories == null)
			{
				Logger.LogError("Featured dish category returned null");
				return BadRequest();
			}

			return Ok(featuredDuishCategories.ToList());
		}
	}
}
