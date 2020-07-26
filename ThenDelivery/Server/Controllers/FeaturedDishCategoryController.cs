using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.FeaturedDishCategoryController.Queries;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	public class FeaturedDishCategoryController : CustomControllerBase<FeaturedDishCategoryController>
	{
		[HttpGet]
		public async Task<IActionResult> GetAllFeaturedDishCategory()
		{
			IEnumerable<FeaturedDishCategoryDto> featuredDuishCategories = 
				await Mediator.Send(new GetAllFeaturedDishCategoryQuery());

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
