using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Server.Persistence;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Application.ProductController.Commands
{
	public class UpdateRangeProductCommand : IRequest<Unit>
	{
		private readonly IEnumerable<ProductDto> _productList;

		public UpdateRangeProductCommand(IEnumerable<ProductDto> productList)
		{
			_productList = productList ?? throw new ArgumentNullException(nameof(productList));
		}

		public class Handler : IRequestHandler<UpdateRangeProductCommand, Unit>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<UpdateRangeProductCommand> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<UpdateRangeProductCommand> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<Unit> Handle(UpdateRangeProductCommand request, CancellationToken cancellationToken)
			{
				using var trans = _dbContext.Database.BeginTransaction();
				try
				{
					foreach (var productDto in request._productList)
					{
						var productToInsert = GetProductData(productDto);
						_dbContext.Products.Update(productToInsert);
						await _dbContext.SaveChangesAsync();
						_dbContext.Toppings.UpdateRange(GetToppingData(productDto.ToppingList, productToInsert.Id));
					}

					await _dbContext.SaveChangesAsync();
					await trans.CommitAsync();
				}
				catch (Exception ex)
				{
					await trans.RollbackAsync();
					_logger.LogError(ex, ex.Message);
					throw;
				}
				return Unit.Value;
			}

			private Product GetProductData(ProductDto productDto)
			{
				return new Product()
				{
					Id = productDto.Id,
					Description = productDto.Description,
					FavoriteCount = productDto.FavoriteCount,
					Image = productDto.Image,
					IsAvailable = productDto.IsAvailable,
					MenuItemId = productDto.MenuItem.Id,
					Name = productDto.Name,
					OrderCount = productDto.OrderCount,
					UnitPrice = productDto.UnitPrice,
				};
			}

			private IEnumerable<Topping> GetToppingData(IEnumerable<ToppingDto> toppingDtoList, int productId)
			{
				foreach (var toppingDto in toppingDtoList)
				{
					yield return new Topping()
					{
						Id = toppingDto.Id,
						Name = toppingDto.Name,
						ProductId = productId,
						UnitPrice = toppingDto.UnitPrice
					};
				}
			}
		}
	}
}
