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
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Application.MerchantController.Commands
{
	public class UpdateMerchantCommand : IRequest<Unit>
	{
		private readonly MerchantDto _merchantDto;

		public UpdateMerchantCommand(MerchantDto merchantDto)
		{
			_merchantDto = merchantDto;
		}

		public class Handler : IRequestHandler<UpdateMerchantCommand, Unit>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<UpdateMerchantCommand> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<UpdateMerchantCommand> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<Unit> Handle(UpdateMerchantCommand request, CancellationToken cancellationToken)
			{
            using var trans = _dbContext.Database.BeginTransaction();
            try
            {
               Merchant merchantToUpdate = GetMerchant(request._merchantDto);
					//Merchant merchant = await _dbContext.Merchants.SingleOrDefaultAsync(e => e.Id == merchantToUpdate.Id);
					//merchant = merchantToAdd;
					//_dbContext.Attach(merchantToUpdate);
					_dbContext.Update(merchantToUpdate);
               await _dbContext.SaveChangesAsync();
               request._merchantDto.Id = merchantToUpdate.Id;

               // Insert to many to many table with merchant type
               _dbContext.MTMerchants.RemoveRange(_dbContext.MTMerchants.Where(e => e.MerchantId == merchantToUpdate.Id));
               List<MTMerchant> merchantTypeMerchantToAdd =
                  GetMerchantTypes(request._merchantDto);
               await _dbContext.MTMerchants.AddRangeAsync(merchantTypeMerchantToAdd);

               // Insert to many to many table with featured dish category
               _dbContext.FDMerchants.RemoveRange(_dbContext.FDMerchants.Where(e => e.MerchantId == merchantToUpdate.Id));
               List<FDMerchant> featuredDishCategoryMerchantToAdd =
                  GetFeaturedDishCategoies(request._merchantDto);
               await _dbContext.FDMerchants.AddRangeAsync(featuredDishCategoryMerchantToAdd);
               await _dbContext.SaveChangesAsync();

               await trans.CommitAsync();
               return Unit.Value;
            }
            catch (Exception ex)
            {
               await trans.RollbackAsync();
               _logger.LogError(ex, "Update fail: {0}", request._merchantDto);
               throw;
            }
         }

			private Merchant GetMerchant(MerchantDto merchantDto)
			{
				var merchantResult = new Merchant
				{
					UserId = merchantDto.User.Id,
					Name = merchantDto.Name,
					Description = merchantDto.Description,
					Avatar = merchantDto.Avatar,
					CoverPicture = merchantDto.CoverPicture,
					HouseNumber = merchantDto.HouseNumber,
					PhoneNumber = merchantDto.PhoneNumber,
					SearchKey = merchantDto.SearchKey,
					TaxCode = merchantDto.TaxCode,
					OpenTime = merchantDto.OpenTime.ToStringWithoutDelimiter(),
					CloseTime = merchantDto.CloseTime.ToStringWithoutDelimiter(),
					CityCode = merchantDto.City.CityCode,
					DistrictCode = merchantDto.District.DistrictCode,
					WardCode = merchantDto.Ward.WardCode,
					Id = merchantDto.Id,
					Status = (byte)merchantDto.Status
				};
				return merchantResult;
			}

			/// <summary>
			/// Only call this method when DbContext.SaveChange() was call
			/// because use merchant id
			/// </summary>
			/// <param name="MerchantDto"></param>
			/// <returns>List of MerchantTypeMerchant</returns>
			private List<MTMerchant> GetMerchantTypes(MerchantDto merchantDto)
			{
				var result = new List<MTMerchant>();
				foreach (var typeItem in merchantDto.MerTypeList)
				{
					result.Add(new MTMerchant()
					{
						MerchantTypeId = typeItem.Id,
						MerchantId = merchantDto.Id
					});
				}
				return result;
			}

			/// <summary>
			/// Only call this method when DbContext.SaveChange() was call
			/// because use merchant id
			/// </summary>
			/// <param name="MerchantDto"></param>
			/// <returns>List of FeaturedDishCategoryMerchant</returns>
			private List<FDMerchant> GetFeaturedDishCategoies(MerchantDto merchantDto)
			{
				var result = new List<FDMerchant>();
				foreach (var typeItem in merchantDto.FeaturedDishList)
				{
					result.Add(new FDMerchant()
					{
						FeaturedDishId = typeItem.Id,
						MerchantId = merchantDto.Id
					});
				}
				return result;
			}
		}
	}
}
