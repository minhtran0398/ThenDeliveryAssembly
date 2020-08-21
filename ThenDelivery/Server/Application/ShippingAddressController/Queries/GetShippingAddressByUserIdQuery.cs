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

namespace ThenDelivery.Server.Application.ShippingAddressController.Queries
{
	public class GetShippingAddressByUserIdQuery : IRequest<IEnumerable<ShippingAddressDto>>
	{
		private readonly string _userId;

		public GetShippingAddressByUserIdQuery(string userId)
		{
			_userId = userId;
		}

		public class Handler : IRequestHandler<GetShippingAddressByUserIdQuery, IEnumerable<ShippingAddressDto>>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<GetShippingAddressByUserIdQuery> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<GetShippingAddressByUserIdQuery> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<IEnumerable<ShippingAddressDto>> Handle(GetShippingAddressByUserIdQuery request, CancellationToken cancellationToken)
			{
				try
				{
					return await (from address in _dbContext.ShippingAddresses
									  where address.UserId == request._userId
									  let queryCity = (from city in _dbContext.Cities
															 where city.CityCode == address.CityCode
															 join lv in _dbContext.CityLevels
																  on city.CityLevelId equals lv.Id
															 select new CityDto()
															 {
																 CityCode = city.CityCode,
																 Name = city.Name,
																 CityLevelId = lv.Id,
																 CityLevelName = lv.Name
															 })
									  let queryDistrict = (from district in _dbContext.Districts
																  where district.DistrictCode == address.DistrictCode
																  join lv in _dbContext.DistrictLevels
																		on district.DistrictLevelId equals lv.Id
																  select new DistrictDto()
																  {
																	  CityCode = district.CityCode,
																	  DistrictCode = district.DistrictCode,
																	  Name = district.Name,
																	  DistrictLevelId = lv.Id,
																	  DistrictLevelName = lv.Name
																  })
									  let queryWard = (from ward in _dbContext.Wards
															 where ward.WardCode == address.WardCode
															 join lv in _dbContext.WardLevels
																  on ward.WardLevelId equals lv.Id
															 select new WardDto()
															 {
																 WardCode = ward.WardCode,
																 DistrictCode = ward.DistrictCode,
																 Name = ward.Name,
																 WardLevelId = lv.Id,
																 WardLevelName = lv.Name
															 })
									  select new ShippingAddressDto()
									  {
										  Id = address.Id,
										  UserId = address.UserId,
										  City = queryCity.SingleOrDefault(),
										  District = queryDistrict.SingleOrDefault(),
										  Ward = queryWard.SingleOrDefault(),
										  HouseNumber = address.HouseNumber,
										  FullName = address.FullName,
										  PhoneNumber = address.PhoneNumber
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
