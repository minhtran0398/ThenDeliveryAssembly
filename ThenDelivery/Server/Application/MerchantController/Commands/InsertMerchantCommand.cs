using DevExpress.Blazor.Internal;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
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
						request._merchantDto.MerchantId = merchantToAdd.MerchantId;

						// Insert to many to many table with merchant type
						List<MerchantTypeMerchant> merchantTypeMerchantToAdd = 
							GetMerchantTypes(request._merchantDto);
						await _dbContext.MerchantTypeMerchants.AddRangeAsync(merchantTypeMerchantToAdd);

						// Insert to many to many table with featured dish category
						List<FeaturedDishCategoryMerchant> featuredDishCategoryMerchantToAdd =
							GetFeaturedDishCategoies(request._merchantDto);
						await _dbContext.FeaturedDishCategoryMerchants.AddRangeAsync(featuredDishCategoryMerchantToAdd);
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
				return request._merchantDto.MerchantId;
			}

			private Merchant GetMerchant(MerchantDto merchantDto)
			{
				var merchantResult = new Merchant();
				merchantResult.UserId = merchantDto.UserId;
				merchantResult.Name = merchantDto.Name;
				merchantResult.Description = merchantDto.Description;
				merchantResult.Avatar = merchantDto.Avatar;
				merchantResult.CoverPicture = merchantDto.CoverPicture;
				merchantResult.HouseNumber = merchantDto.HouseNumber;
				merchantResult.PhoneNumber = merchantDto.PhoneNumber;
				merchantResult.SearchKey = merchantDto.SearchKey;
				merchantResult.TaxCode = merchantDto.TaxCode;
				merchantResult.OpenTime = merchantDto.OpenTime.ToStringWithoutDelimiter();
				merchantResult.CloseTime = merchantDto.CloseTime.ToStringWithoutDelimiter();
				merchantResult.CityCode = merchantDto.City.CityCode;
				merchantResult.DistrictCode = merchantDto.District.DistrictCode;
				merchantResult.WardCode = merchantDto.Ward.WardCode;
				return merchantResult;
			}

			/// <summary>
			/// Only call this method when DbContext.SaveChange() was call
			/// because use merchant id
			/// </summary>
			/// <param name="MerchantDto"></param>
			/// <returns>List of MerchantTypeMerchant</returns>
			private List<MerchantTypeMerchant> GetMerchantTypes(MerchantDto merchantDto)
			{
				var result = new List<MerchantTypeMerchant>();
				foreach (var typeItem in merchantDto.MerchantTypeList)
				{
					result.Add(new MerchantTypeMerchant()
					{
						MerchantTypeId = typeItem.MerchantTypeId,
						MerchantId = merchantDto.MerchantId
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
			private List<FeaturedDishCategoryMerchant> GetFeaturedDishCategoies(MerchantDto merchantDto)
			{
				var result = new List<FeaturedDishCategoryMerchant>();
				foreach (var typeItem in merchantDto.FeaturedDishCategoryList)
				{
					result.Add(new FeaturedDishCategoryMerchant()
					{
						FeaturedDishCategoryId = typeItem.FeaturedDishCategoryId,
						MerchantId = merchantDto.MerchantId
					});
				}
				return result;
			}
		}
	}
}
