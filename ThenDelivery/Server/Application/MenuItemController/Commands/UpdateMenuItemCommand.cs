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
					var menuItemListDb = await _dbContext.MenuItems
						.Where(e => e.MerchantId == request._merchantVM.MerchantId)
						.AsNoTracking().ToListAsync();
					var newMenuItemList = GetMenuItemList(request._merchantVM);

					// case delete
					var listToDelete = menuItemListDb.Except(newMenuItemList, new MenuIdComparer());
               foreach (var menuItem in listToDelete)
               {
						var productToDelete = _dbContext.Products.Where(e => e.MenuItemId == menuItem.Id);
						_dbContext.RemoveRange(productToDelete);
               }
					_dbContext.MenuItems.RemoveRange(listToDelete);
					await _dbContext.SaveChangesAsync();

					foreach (var newMenuItem in newMenuItemList)
					{
						var menuItemDb = menuItemListDb.SingleOrDefault(e => e.Id == newMenuItem.Id);
						// case edit menu Item
						if (menuItemDb != null)
						{
							menuItemDb.SetData(newMenuItem);
							var productList = GetProductList(request._merchantVM, menuItemDb.Id);
						}
						// case insert
                  else
                  {

                  }
					}

					await trans.CommitAsync();
					return Unit.Value;
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, ex.Message);
					throw;
				}
			}

			public IEnumerable<MenuItem> GetMenuItemList(EditMerchantVM merchantVM)
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

			public IEnumerable<Product> GetProductList(EditMerchantVM merchantVM, int menuItemId)
			{
            foreach (var product in merchantVM.MenuItemList.SingleOrDefault(e => e.Id == menuItemId).ProductList)
            {
               yield return new Product()
               {
                  Id = product.Id,
                  FavoriteCount = product.FavoriteCount,
                  Description = product.Description,
                  Image = _imageService.SaveImage(product.Image, "Product"),
                  IsAvailable = product.IsAvailable,
                  MenuItemId = menuItemId,
                  Name = product.Name,
                  OrderCount = product.OrderCount,
                  UnitPrice = product.UnitPrice
               };
            }
			}
		}
	}
}
