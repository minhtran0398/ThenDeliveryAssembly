using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.ProductController.Commands;
using ThenDelivery.Server.Application.ProductController.Queries;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Server.Application.Common.Interfaces;

namespace ThenDelivery.Server.Controllers
{
	[AllowAnonymous]
	public class ProductController : CustomControllerBase<ProductController>
	{
		private readonly IImageService _imageService;

		public ProductController(IImageService imageService)
		{
			_imageService = imageService;
		}

		[HttpPost]
		[Authorize(Roles = Const.Role.MerchantRole)]
		public async Task<IActionResult> AddRangeProduct(IEnumerable<ProductDto> productList)
		{
			try
			{
				foreach (var product in productList)
				{
					product.Image = _imageService.SaveImage(product.Image, "Product");
				}

				await Mediator.Send(new InsertRangeProductCommand(productList));
				return Ok("Insert products success");
			}
			catch (ArgumentNullException ex)
			{
				Logger.LogError(ex, ex.Message);
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				Logger.LogError(ex, ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetProductsByMerchantId(int merchantId)
		{
			try
			{
				IEnumerable<ProductDto> productList =
					await Mediator.Send(new GetProductsByMerchantId(merchantId));
				return Ok(productList);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
