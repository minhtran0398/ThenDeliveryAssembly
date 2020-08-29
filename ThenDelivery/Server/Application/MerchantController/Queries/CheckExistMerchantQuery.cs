using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Server.Persistence;

namespace ThenDelivery.Server.Application.MerchantController.Queries
{
   public class CheckExistMerchantQuery : IRequest<bool>
   {
      private readonly int _merchantId;
      private readonly string _userId;

      public CheckExistMerchantQuery(int merchantId, string userId)
      {
         _merchantId = merchantId;
         _userId = userId;
      }

      public class Handler : IRequestHandler<CheckExistMerchantQuery, bool>
      {
         private readonly ThenDeliveryDbContext _dbContext;
         private readonly ILogger<CheckExistMerchantQuery> _logger;

         public Handler(ThenDeliveryDbContext dbContext, ILogger<CheckExistMerchantQuery> logger)
         {
            _dbContext = dbContext;
            _logger = logger;
         }

         public async Task<bool> Handle(CheckExistMerchantQuery request, CancellationToken cancellationToken)
         {
            try
            {
               var merchants = await _dbContext.Merchants.Where(e => e.Id == request._merchantId && e.UserId == request._userId).ToListAsync();
               return merchants.Count != 0;
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
