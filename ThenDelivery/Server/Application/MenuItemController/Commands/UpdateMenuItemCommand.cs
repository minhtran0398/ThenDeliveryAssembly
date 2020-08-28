using DevExpress.Blazor.Internal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.Common.Interfaces;
using ThenDelivery.Server.Persistence;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Application.MenuItemController.Commands
{
	public class UpdateMenuItemCommand : IRequest<Unit>
	{
		private readonly EditMerchantVM _merchantVM;

		public UpdateMenuItemCommand(EditMerchantVM merchantVM)
		{
			_merchantVM = merchantVM;
		}

		public class Handler : IRequestHandler<UpdateMenuItemCommand, Unit>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<UpdateMenuItemCommand> _logger;
			private readonly IImageService _imageService;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<UpdateMenuItemCommand> logger,
				IImageService imageService)
			{
				_dbContext = dbContext;
				_logger = logger;
				_imageService = imageService;
			}

			public async Task<Unit> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
			{
				using var trans = _dbContext.Database.BeginTransaction();
				try
				{
					var menuItemList = await _dbContext.MenuItems
						.Where(e => e.MerchantId == request._merchantVM.MerchantId)
						.AsNoTracking().ToListAsync();
					var productList = await _dbContext.Products
						.AsNoTracking().ToListAsync();
					var newList = GetMenuItem(request._merchantVM);
					var newProductList = GetProduct(request._merchantVM);

					// case delete
					foreach (var menuItem in menuItemList)
					{
						if (newList.Any(e => e.Id == menuItem.Id) == false)
						{
							_dbContext.MenuItems.Remove(menuItem);
							var products = _dbContext.Products.Where(e => e.MenuItemId == menuItem.Id);
							foreach (var product in products)
							{
							}
						}
					}
					foreach (var menuItem in newList)
					{
						// case edit menu Item
						if (menuItemList.Any(e => e.Id == menuItem.Id))
						{

						}

					}

					// case edit
					var listToUpdate = menuItemList.Join(newList, o => o.Id, n => n.Id, (o, n) => new MenuItem()
					{
						Id = n.Id,
						Description = n.Description,
						MerchantId = n.MerchantId,
						Name = n.Name
					});
					_dbContext.MenuItems.UpdateRange(listToUpdate);
					await _dbContext.SaveChangesAsync();
					var listProductToUpdate = productList.Join(newProductList, o => o.Id, n => n.Id, (o, n) => new Product()
					{
						Id = n.Id,
						FavoriteCount = n.FavoriteCount,
						Description = n.Description,
						Image = n.Image,
						IsAvailable = n.IsAvailable,
						MenuItemId = n.Id,
						Name = n.Name,
						OrderCount = n.OrderCount,
						UnitPrice = n.UnitPrice
					});
					_dbContext.Products.UpdateRange(listProductToUpdate);
					await _dbContext.SaveChangesAsync();

					// case delete
					var listToDelete = menuItemList.Except(newList, new MenuIdComparer());
					_dbContext.MenuItems.RemoveRange(listToDelete);
					await _dbContext.SaveChangesAsync();
					var listProductToDelete = productList.Except(newProductList, new ProductIdComparer());
					_dbContext.Products.RemoveRange(listProductToDelete);
					await _dbContext.SaveChangesAsync();

					// case insert
					var listToInsert = newList.Except(menuItemList, new MenuIdComparer()).Select(n => new MenuItem()
					{
						Id = 0,
						Description = n.Description,
						MerchantId = n.MerchantId,
						Name = n.Name
					});
					await _dbContext.MenuItems.AddRangeAsync(listToInsert);
					await _dbContext.SaveChangesAsync();
					var listProductToInsert = newProductList.Except(productList, new ProductIdComparer()).Select(n => new Product()
					{
						Id = 0,
						FavoriteCount = n.FavoriteCount,
						Description = n.Description,
						Image = n.Image,
						IsAvailable = n.IsAvailable,
						MenuItemId = n.Id,
						Name = n.Name,
						OrderCount = n.OrderCount,
						UnitPrice = n.UnitPrice
					});
					await _dbContext.Products.AddRangeAsync(listProductToInsert);
					await _dbContext.SaveChangesAsync();

					await trans.CommitAsync();
					return Unit.Value;
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, ex.Message);
					throw;
				}
			}

			public IEnumerable<MenuItem> GetMenuItem(EditMerchantVM merchantVM)
			{
				foreach (var item in merchantVM.MenuItemList)
				{
					yield return new MenuItem()
					{
						Id = item.Id,
						Description = item.Description,
						MerchantId = item.MerchantId,
						Name = item.Name
					};
				}
			}

			public IEnumerable<Product> GetProduct(EditMerchantVM merchantVM)
			{
				foreach (var item in merchantVM.MenuItemList)
				{
					foreach (var product in item.ProductList)
					{
						yield return new Product()
						{
							Id = product.Id,
							FavoriteCount = product.FavoriteCount,
							Description = product.Description,
							Image = _imageService.SaveImage(product.Image, "Product"),
							IsAvailable = product.IsAvailable,
							MenuItemId = item.Id,
							Name = product.Name,
							OrderCount = product.OrderCount,
							UnitPrice = product.UnitPrice
						};
					}
				}
			}
		}
	}
}
