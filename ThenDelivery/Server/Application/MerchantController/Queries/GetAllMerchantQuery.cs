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
using ThenDelivery.Shared.Helper;

namespace ThenDelivery.Server.Application.MerchantController.Queries
{
	public class GetAllMerchantQuery : IRequest<IEnumerable<MerchantDto>>
	{
		private readonly ushort _skip;
		private readonly ushort _take;

		public GetAllMerchantQuery(ushort skip = 0, ushort take = 0)
		{
			_skip = skip;
			_take = take;
		}

		public class Handler : IRequestHandler<GetAllMerchantQuery, IEnumerable<MerchantDto>>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<GetAllMerchantQuery> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<GetAllMerchantQuery> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<IEnumerable<MerchantDto>> Handle(GetAllMerchantQuery request, CancellationToken cancellationToken)
			{
				var result = new List<MerchantDto>();
				try
				{
					if (request._take > 0)
					{
						result = await (from merchant in _dbContext.Merchants
											 let queryType = (from mtype in _dbContext.MTMerchants
																	where mtype.MerchantId == merchant.Id
																	join type in _dbContext.MerTypes
																		  on mtype.MerchantTypeId equals type.Id
																	select new MerTypeDto
																	{
																		Id = type.Id,
																		Name = type.Name
																	})
											 let queryDish = (from mdish in _dbContext.FDMerchants
																	where mdish.MerchantId == merchant.Id
																	join dish in _dbContext.FeaturedDishes
																		  on mdish.FeaturedDishId equals dish.Id
																	select new FeaturedDishDto
																	{
																		Id = dish.Id,
																		Name = dish.Name
																	})
											 let queryCity = (from city in _dbContext.Cities
																	where city.CityCode == merchant.CityCode
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
																		 where district.DistrictCode == merchant.DistrictCode
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
																	where ward.WardCode == merchant.WardCode
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
											 select new MerchantDto()
											 {
												 Id = merchant.Id,
												 Name = merchant.Name,
												 Avatar = merchant.Avatar,
												 City = queryCity.SingleOrDefault(),
												 District = queryDistrict.SingleOrDefault(),
												 Ward = queryWard.SingleOrDefault(),
												 OpenTime = GetTime(merchant.OpenTime),
												 CloseTime = GetTime(merchant.CloseTime),
												 CoverPicture = merchant.CoverPicture,
												 Description = merchant.Description,
												 MerchantTypeList = queryType.ToList(),
												 FeaturedDishCategoryList = queryDish.ToList(),
												 HouseNumber = merchant.HouseNumber,
												 PhoneNumber = merchant.PhoneNumber,
												 SearchKey = merchant.SearchKey,
												 TaxCode = merchant.TaxCode,
												 UserId = merchant.UserId
											 })
											.Skip(request._skip)
											.Take(request._take)
											.ToListAsync();
					}
					else
					{
						result = await (from merchant in _dbContext.Merchants
											 let queryType = (from mtype in _dbContext.MTMerchants
																	where mtype.MerchantId == merchant.Id
																	join type in _dbContext.MerTypes
																		  on mtype.MerchantTypeId equals type.Id
																	select new MerTypeDto
																	{
																		Id = type.Id,
																		Name = type.Name
																	})
											 let queryDish = (from mdish in _dbContext.FDMerchants
																	where mdish.MerchantId == merchant.Id
																	join dish in _dbContext.FeaturedDishes
																		  on mdish.FeaturedDishId equals dish.Id
																	select new FeaturedDishDto
																	{
																		Id = dish.Id,
																		Name = dish.Name
																	})
											 let queryCity = (from city in _dbContext.Cities
																	where city.CityCode == merchant.CityCode
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
																		 where district.DistrictCode == merchant.DistrictCode
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
																	where ward.WardCode == merchant.WardCode
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
											 select new MerchantDto()
											 {
												 Id = merchant.Id,
												 Name = merchant.Name,
												 Avatar = merchant.Avatar,
												 City = queryCity.SingleOrDefault(),
												 District = queryDistrict.SingleOrDefault(),
												 Ward = queryWard.SingleOrDefault(),
												 OpenTime = GetTime(merchant.OpenTime),
												 CloseTime = GetTime(merchant.CloseTime),
												 CoverPicture = merchant.CoverPicture,
												 Description = merchant.Description,
												 MerchantTypeList = queryType.ToList(),
												 FeaturedDishCategoryList = queryDish.ToList(),
												 HouseNumber = merchant.HouseNumber,
												 PhoneNumber = merchant.PhoneNumber,
												 SearchKey = merchant.SearchKey,
												 TaxCode = merchant.TaxCode,
												 UserId = merchant.UserId
											 })
											.Skip(request._skip)
											.ToListAsync();
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);
					return null;
				}
				return result;
			}

			private static CustomTime GetTime(string time)
			{
				return new CustomTime(Int16.Parse(time.Substring(0, 2)), Int16.Parse(time.Substring(2)));
			}
		}
	}
}
