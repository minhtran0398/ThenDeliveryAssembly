using MediatR;
using Microsoft.Extensions.Logging;
using System;
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
				int createdId = -1;
				using (var trans = _dbContext.Database.BeginTransaction())
				{
					try
					{
						var merchantToAdd = GetMerchant(request._merchantDto);

						await _dbContext.Merchants.AddAsync(merchantToAdd);
						await _dbContext.SaveChangesAsync();
						createdId = merchantToAdd.MerchantId;

						await trans.CommitAsync();
					}
					catch (Exception ex)
					{
						await trans.RollbackAsync();
						_logger.LogError(ex, "Insert fail: {0}", request._merchantDto);
						return -1;
					}
				}
				return createdId;
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
		}
	}
}
