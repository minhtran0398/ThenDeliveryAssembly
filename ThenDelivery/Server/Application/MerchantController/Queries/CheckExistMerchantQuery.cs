using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Server.Persistence;

namespace ThenDelivery.Server.Application.MerchantController.Queries
{
   public class CheckExistMerchantQuery : IRequest<bool>
   {
      private readonly int _merchantId;

      public CheckExistMerchantQuery(int merchantId)
      {
         _merchantId = merchantId;
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
               var merchant = await _dbContext.Merchants.FindAsync(request._merchantId);
               return merchant != null;
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
