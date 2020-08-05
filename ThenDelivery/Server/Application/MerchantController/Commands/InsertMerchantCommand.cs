using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Server.Persistence;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Application.MerchantController.Commands
{
	public class InsertMerchantCommand : IRequest<int>
	{
		private readonly MerchantDto _merchantDto;

		public InsertMerchantCommand(MerchantDto merchantDto)
		{
			_merchantDto = merchantDto;
		}

		public class Handler : IRequestHandler<InsertMerchantCommand, int>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<InsertMerchantCommand> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<InsertMerchantCommand> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<int> Handle(InsertMerchantCommand request, CancellationToken cancellationToken)
			{
				using (var trans = _dbContext.Database.BeginTransaction())
				{
					try
					{
						Merchant merchantToAdd = GetMerchant(request._merchantDto);

						await _dbContext.Merchants.AddAsync(merchantToAdd);
						await _dbContext.SaveChangesAsync();
						request._merchantDto.Id = merchantToAdd.Id;

						// Insert to many to many table with merchant type
						List<MTMerchant> merchantTypeMerchantToAdd =
							GetMerchantTypes(request._merchantDto);
						await _dbContext.MTMerchants.AddRangeAsync(merchantTypeMerchantToAdd);

						// Insert to many to many table with featured dish category
						List<FDMerchant> featuredDishCategoryMerchantToAdd =
							GetFeaturedDishCategoies(request._merchantDto);
						await _dbContext.FDMerchants.AddRangeAsync(featuredDishCategoryMerchantToAdd);
						await _dbContext.SaveChangesAsync();

						await trans.CommitAsync();
					}
					catch (Exception ex)
					{
						await trans.RollbackAsync();
						_logger.LogError(ex, "Insert fail: {0}", request._merchantDto);
						return -1;
					}
				}
				return request._merchantDto.Id;
			}

			private Merchant GetMerchant(MerchantDto merchantDto)
			{
				var merchantResult = new Merchant
				{
					UserId = merchantDto.UserId,
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
					WardCode = merchantDto.Ward.WardCode
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
