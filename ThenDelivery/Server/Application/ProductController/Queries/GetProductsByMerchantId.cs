using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Server.Persistence;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Server.Application.ProductController.Queries
{
	public class GetProductsByMerchantId : IRequest<IEnumerable<ProductDto>>
	{
		private readonly int _merchantId;

		public GetProductsByMerchantId(int merchantId)
		{
			_merchantId = merchantId;
		}

		public class Handler : IRequestHandler<GetProductsByMerchantId, IEnumerable<ProductDto>>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<GetProductsByMerchantId> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<GetProductsByMerchantId> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<IEnumerable<ProductDto>> Handle(GetProductsByMerchantId request, CancellationToken cancellationToken)
			{
				try
				{
					return await _dbContext.Products
						.Where(p => p.MerchantId == request._merchantId)
						.Select(p => new ProductDto()
						{
							Id = p.Id,
							Description = p.Description,
							FavoriteCount = p.FavoriteCount,
							Image = p.Image,
							IsAvailable = p.IsAvailable,
							Name = p.Name,
							OrderCount = p.OrderCount,
							UnitPrice = p.UnitPrice,
							MenuItem = _dbContext.MenuItems
											.Where(m => m.Id == p.MenuItemId)
											.Select(m => new MenuItemDto()
											{
												Id = m.Id,
												Description = m.Description,
												MerchantId = m.MerchantId,
												Name = m.Name
											}).Single(),
							Merchant = new MerchantDto()
							{
								Id = request._merchantId
							},
							ToppingList = _dbContext.Toppings
											.Where(t => t.ProductId == p.Id)
											.Select(t => new ToppingDto()
											{
												Id = t.Id,
												Name = t.Name,
												ProductId = t.ProductId,
												UnitPrice = t.UnitPrice,
											}).ToList()
						})
						.ToListAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, ex.Message);
					throw;
				}
			}
		}
	}
}
