using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Server.Persistence;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Entities;
using ThenDelivery.Shared.Enums;

namespace ThenDelivery.Server.Application.MerchantController.Commands
{
   public class UpdateMerchantStatusCommand : IRequest<Unit>
   {
      private readonly int _merchantId;
      private readonly MerchantStatus _status;

      public UpdateMerchantStatusCommand(int merchantId, MerchantStatus status)
      {
         _merchantId = merchantId;
         _status = status;
      }

      public class Handler : IRequestHandler<UpdateMerchantStatusCommand, Unit>
      {
         private readonly ThenDeliveryDbContext _dbContext;
         private readonly ILogger<UpdateMerchantStatusCommand> _logger;
         private readonly UserManager<User> _userManager;

         public Handler(ThenDeliveryDbContext dbContext,
            ILogger<UpdateMerchantStatusCommand> logger,
            UserManager<User> userManager)
         {
            _dbContext = dbContext;
            _logger = logger;
            _userManager = userManager;
         }

         public async Task<Unit> Handle(UpdateMerchantStatusCommand request, CancellationToken cancellationToken)
         {
            using var trans = _dbContext.Database.BeginTransaction();
            try
            {
               var merchant = await _dbContext.Merchants.SingleOrDefaultAsync(e => e.Id == request._merchantId);
               merchant.Status = (byte)request._status;
               if(request._status == MerchantStatus.Approved)
               {
                  var user = await _dbContext.Users.FindAsync(merchant.UserId);
                  await _userManager.AddToRoleAsync(user, Const.Role.MerchantRole);
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
      }
   }
}
