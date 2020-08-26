﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Server.Persistence;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Enums;
using ThenDelivery.Shared.Helper;

namespace ThenDelivery.Server.Application.MerchantController.Queries
{
	public class GetAllMerchantQuery : IRequest<IEnumerable<MerchantDto>>
	{
		private readonly ushort _skip;
		private readonly ushort _take;
		private readonly MerchantStatus _status;

		public GetAllMerchantQuery(MerchantStatus status = MerchantStatus.Approved, ushort skip = 0, ushort take = 0)
		{
			_skip = skip;
			_take = take;
			_status = status;
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
					
					if(request._status == MerchantStatus.None)
               {
						var query = (from merchant in _dbContext.Merchants
                               join user in _dbContext.Users on merchant.UserId equals user.Id
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
											 OpenTime = CustomTime.GetTime(merchant.OpenTime),
											 CloseTime = CustomTime.GetTime(merchant.CloseTime),
											 CoverPicture = merchant.CoverPicture,
											 Description = merchant.Description,
											 MerTypeList = queryType.ToList(),
											 FeaturedDishList = queryDish.ToList(),
											 HouseNumber = merchant.HouseNumber,
											 PhoneNumber = merchant.PhoneNumber,
											 SearchKey = merchant.SearchKey,
											 TaxCode = merchant.TaxCode,
											 LastModify = merchant.LastModified,
											 CreateTime = merchant.Created,
											 Status = (MerchantStatus)merchant.Status,
											 User = new UserDto()
											 {
												 Id = user.Id,
												 BirthDate = user.BirthDate,
												 Email = user.Email,
												 IsEmailConfirmed = user.EmailConfirmed,
												 IsPhoneNumberConfirmed = user.PhoneNumberConfirmed,
												 PhoneNumber = user.PhoneNumber,
												 UserName = user.UserName,
											 }
										 });

						if (request._take > 0)
						{
							query = query.Take(request._take).Skip(request._skip);
						}
						else
						{
							query = query.Skip(request._skip);
						}

						result = await query.ToListAsync();
					}
               else
               {
						var query = (from merchant in _dbContext.Merchants
                               where merchant.Status == (byte)request._status
                               join user in _dbContext.Users on merchant.UserId equals user.Id
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
											 OpenTime = CustomTime.GetTime(merchant.OpenTime),
											 CloseTime = CustomTime.GetTime(merchant.CloseTime),
											 CoverPicture = merchant.CoverPicture,
											 Description = merchant.Description,
											 MerTypeList = queryType.ToList(),
											 FeaturedDishList = queryDish.ToList(),
											 HouseNumber = merchant.HouseNumber,
											 PhoneNumber = merchant.PhoneNumber,
											 SearchKey = merchant.SearchKey,
											 TaxCode = merchant.TaxCode,
											 LastModify = merchant.LastModified,
											 CreateTime = merchant.Created,
											 Status = (MerchantStatus)merchant.Status,
											 User = new UserDto()
											 {
												 Id = user.Id,
												 BirthDate = user.BirthDate,
												 Email = user.Email,
												 IsEmailConfirmed = user.EmailConfirmed,
												 IsPhoneNumberConfirmed = user.PhoneNumberConfirmed,
												 PhoneNumber = user.PhoneNumber,
												 UserName = user.UserName,
											 }
										 });

						if (request._take > 0)
						{
							query = query.Take(request._take).Skip(request._skip);
						}
						else
						{
							query = query.Skip(request._skip);
						}

						result = await query.ToListAsync();
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);
					throw;
				}
				return result;
			}
		}
	}
}
