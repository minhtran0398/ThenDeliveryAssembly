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

namespace ThenDelivery.Server.Application.MerchantController.Queries
{
   public class GetEditMenuItemQuery : IRequest<IEnumerable<EditMenuItemVM>>
   {
      private readonly int _merchantId;

      public GetEditMenuItemQuery(int merchantId)
      {
         _merchantId = merchantId;
      }

      public class Handler : IRequestHandler<GetEditMenuItemQuery, IEnumerable<EditMenuItemVM>>
      {
         private readonly ThenDeliveryDbContext _dbContext;
         private readonly ILogger<GetEditMenuItemQuery> _logger;

         public Handler(ThenDeliveryDbContext dbContext,
            ILogger<GetEditMenuItemQuery> logger)
         {
            _dbContext = dbContext;
            _logger = logger;
         }

         public async Task<IEnumerable<EditMenuItemVM>> Handle(GetEditMenuItemQuery request, CancellationToken cancellationToken)
         {
            try
            {
               return await (from menu in _dbContext.MenuItems
                             where menu.MerchantId == request._merchantId && menu.IsDeleted == false
                             let query = from product in _dbContext.Products
                                         where product.MenuItemId == menu.Id && product.IsDeleted == false
                                         let queryT = from top in _dbContext.Toppings
                                                      where top.ProductId == product.Id && top.IsDeleted == false
                                                      select new ToppingDto()
                                                      {
                                                         Id = top.Id,
                                                         Name = top.Name,
                                                         ProductId = top.ProductId,
                                                         UnitPrice = top.UnitPrice
                                                      }
                                         select new EditProductVM()
                                         {
                                            Id = product.Id,
                                            Description = product.Description,
                                            FavoriteCount = product.FavoriteCount,
                                            Image = product.Image,
                                            IsAvailable = product.IsAvailable,
                                            Name = product.Name,
                                            OrderCount = product.OrderCount,
                                            UnitPrice = product.UnitPrice,
                                            ToppingList = queryT.ToList(),
                                         }
                             select new EditMenuItemVM()
                             {
                                Id = menu.Id,
                                Name = menu.Name,
                                Description = menu.Description,
                                MerchantId = menu.MerchantId,
                                ProductList = query.ToList()
                             }).ToListAsync();
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
