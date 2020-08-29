using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.FeaturedDishCategoryController.Queries;
using ThenDelivery.Shared.Dtos;
using System;
using ThenDelivery.Shared.Exceptions;
using System.Linq;

namespace ThenDelivery.Server.Controllers
{
	public class FeaturedDishController : CustomControllerBase<FeaturedDishController>
	{
		[HttpGet]
		public async Task<IActionResult> GetAllFeaturedDishCategory(int id = 0)
		{
			try
			{
				IEnumerable<FeaturedDishDto> featuredDuishCategories
							 = await Mediator.Send(new GetAllFeaturedDishQuery(id));

				if (id == 0)
				{
					return Ok(featuredDuishCategories);
				}
				else
				{
					return Ok(featuredDuishCategories.ToList()?[0]);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(new CustomResponse(400, ex.Message));
			}
		}
	}
}
