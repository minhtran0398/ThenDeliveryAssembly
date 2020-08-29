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

			public Handler(ThenDeliveryDbContext dbContext, ILogger<UpdateMenuItemCommand> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
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

					foreach (var newMenuItem in newMenuItemList)
					{
						// case delete
						if(newMenuItem.IsDeleted)
                  {
							var mn = menuItemListDb.SingleOrDefault(e => e.Id == newMenuItem.Id);
							mn.IsDeleted = true;
							mn.Name = newMenuItem.Name;
							var pdList = GetProductList(request._merchantVM, mn.Id);
							foreach (var product in pdList)
                     {
								var toppingRm = _dbContext.Toppings.Where(e => e.ProductId == product.Id)
														.Select(e => new Topping()
														{
															Id = e.Id,
															Name = e.Name,
															ProductId = e.ProductId,
															IsDeleted = true,
															UnitPrice = e.UnitPrice
														});
								_dbContext.Toppings.UpdateRange(toppingRm);
                     }
							_dbContext.Products.UpdateRange(pdList);
							_dbContext.MenuItems.Update(mn);
							await _dbContext.SaveChangesAsync();

							continue;
                  }
						var menuItemDb = menuItemListDb.SingleOrDefault(e => e.Id == newMenuItem.Id);
						// case edit menu Item
						if (menuItemDb != null)
						{
                     menuItemDb.SetData(newMenuItem);
                     var newProductList = GetProductList(request._merchantVM, menuItemDb.Id);
							var productDb = _dbContext.Products.Where(e => e.MenuItemId == menuItemDb.Id);
                     foreach (var newProduct in newProductList)
                     {
								// edit product
								if(productDb.Any(e => e.Id == newProduct.Id))
                        {
									var pd = productDb.SingleOrDefault(e => e.Id == newProduct.Id);
									pd.SetData(newProduct);
									await _dbContext.SaveChangesAsync();
									var newtoppinglist = GetToppingList(request._merchantVM, menuItemDb.Id, pd.Id);
									var toppingdb = _dbContext.Toppings.Where(e => e.ProductId == pd.Id);
                           foreach (var newTopping in newtoppinglist)
                           {
										//edit topping
										if(toppingdb.Any(e => e.Id == newTopping.Id))
                              {
											var tp = toppingdb.SingleOrDefault(e => e.Id == newTopping.Id);
											tp.SetData(newTopping);
											await _dbContext.SaveChangesAsync();
										}
                              //insety toppping
                              else
                              {
											newTopping.Id = 0;
											newTopping.ProductId = pd.Id;
											await _dbContext.Toppings.AddAsync(newTopping);
											await _dbContext.SaveChangesAsync();
										}
                           }

									await _dbContext.SaveChangesAsync();
                        }
								// insert product
                        else
                        {
									int oldProductId = newProduct.Id;

									var newToppingList = GetToppingList(request._merchantVM, menuItemDb.Id, oldProductId);
									newProduct.Id = 0;
									newProduct.MenuItemId = newMenuItem.Id;
									await _dbContext.Products.AddAsync(newProduct);
									await _dbContext.SaveChangesAsync();
									await _dbContext.Toppings.AddRangeAsync(newToppingList.Select(e => new Topping()
									{
										Id = 0,
										Name = e.Name,
										ProductId = newProduct.Id,
										UnitPrice = e.UnitPrice,
									}));
									await _dbContext.SaveChangesAsync();
								}
                     }
                  }
						// case insert
                  else
                  {
							int oldMenuItemId = newMenuItem.Id;

							var newProductList = GetProductList(request._merchantVM, oldMenuItemId);
							newMenuItem.Id = 0;
							await _dbContext.MenuItems.AddAsync(newMenuItem);
							await _dbContext.SaveChangesAsync();
                     foreach (var newProduct in newProductList)
                     {
								int oldProductId = newProduct.Id;

								var newToppingList = GetToppingList(request._merchantVM, oldMenuItemId, oldProductId);
								newProduct.Id = 0;
								newProduct.MenuItemId = newMenuItem.Id;
								await _dbContext.Products.AddAsync(newProduct);
								await _dbContext.SaveChangesAsync();
								await _dbContext.Toppings.AddRangeAsync(newToppingList.Select(e => new Topping()
								{
									Id = 0,
									Name = e.Name,
									ProductId = newProduct.Id,
									UnitPrice = e.UnitPrice,
								}));
								await _dbContext.SaveChangesAsync();
							}
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
						Name = item.Name,
						IsDeleted = item.IsDelete
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
						Image = product.Image,
						IsAvailable = product.IsAvailable,
                  MenuItemId = menuItemId,
                  Name = product.Name,
                  OrderCount = product.OrderCount,
                  UnitPrice = product.UnitPrice,
						IsDeleted = product.IsDelete,
               };
            }
			}

			public IEnumerable<Topping> GetToppingList(EditMerchantVM merchantVM, int menuItemId, int productId)
			{
				foreach (var topping in merchantVM.MenuItemList
					.SingleOrDefault(e => e.Id == menuItemId).ProductList
					.SingleOrDefault(e => e.Id == productId).ToppingList)
				{
					yield return new Topping()
					{
						Id = topping.Id,
						Name = topping.Name,
						ProductId = productId,
						UnitPrice = topping.UnitPrice,
						IsDeleted = topping.IsDelete
					};
				}
			}
		}
	}
}
