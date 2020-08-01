using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.ProductController.Commands;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Controllers
{
	public class ProductController : CustomControllerBase<ProductController>
	{
		[HttpPost]
		public async Task<IActionResult> AddRangeProduct(IEnumerable<ProductDto> productList)
		{
			try
			{
				await Mediator.Send(new InsertRangeProductCommand(productList));
				return Ok("Insert products success");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
