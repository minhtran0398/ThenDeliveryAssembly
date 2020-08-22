using ThenDelivery.Shared.Helper;
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
using ThenDelivery.Shared.Enums;

namespace ThenDelivery.Server.Application.OrderController.Queries
{
	public class GetOrderHistoryQuery : IRequest<IEnumerable<OrderDto>>
	{
		private readonly string _userId;
		private readonly OrderStatus _orderStatus;

		public GetOrderHistoryQuery(OrderStatus orderStatus, string userId = null)
		{
			_userId = userId;
			_orderStatus = orderStatus;
		}

		public class Handler : IRequestHandler<GetOrderHistoryQuery, IEnumerable<OrderDto>>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<GetOrderHistoryQuery> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<GetOrderHistoryQuery> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<IEnumerable<OrderDto>> Handle(GetOrderHistoryQuery request,
				CancellationToken cancellationToken)
			{
				var result = new List<OrderDto>();
				try
				{
					var queryCity = (from city in _dbContext.Cities
										  join lv in _dbContext.CityLevels
												on city.CityLevelId equals lv.Id
										  select new CityDto()
										  {
											  CityCode = city.CityCode,
											  Name = city.Name,
											  CityLevelId = lv.Id,
											  CityLevelName = lv.Name
										  });
					var queryDistrict = (from district in _dbContext.Districts
												join lv in _dbContext.DistrictLevels
													 on district.DistrictLevelId equals lv.Id
												select new DistrictDto()
												{
													CityCode = district.CityCode,
													DistrictCode = district.DistrictCode,
													Name = district.Name,
													DistrictLevelId = lv.Id,
													DistrictLevelName = lv.Name
												});
					var queryWard = (from ward in _dbContext.Wards
										  join lv in _dbContext.WardLevels
												on ward.WardLevelId equals lv.Id
										  select new WardDto()
										  {
											  WardCode = ward.WardCode,
											  DistrictCode = ward.DistrictCode,
											  Name = ward.Name,
											  WardLevelId = lv.Id,
											  WardLevelName = lv.Name
										  });
					var queryUser = (from u in _dbContext.Users
										  select new UserDto
										  {
											  Id = u.Id,
											  UserName = u.UserName,
											  PhoneNumber = u.PhoneNumber,
											  RoleList = null
										  });
					var queryShippingAddress = from address in _dbContext.ShippingAddresses
														select new ShippingAddressDto()
														{
															Id = address.Id,
															UserId = address.UserId,
															City = queryCity.SingleOrDefault(c => c.CityCode == address.CityCode),
															District = queryDistrict.SingleOrDefault(c => c.DistrictCode == address.DistrictCode),
															Ward = queryWard.SingleOrDefault(c => c.WardCode == address.DistrictCode),
															HouseNumber = address.HouseNumber,
															FullName = address.FullName,
															PhoneNumber = address.PhoneNumber
														};
					var queryOrderItem = from od in _dbContext.OrderDetails
												join p in _dbContext.Products on od.ProductId equals p.Id
												join mi in _dbContext.MenuItems on p.MenuItemId equals mi.Id
												join mer in _dbContext.Merchants on mi.MerchantId equals mer.Id
												select new OrderItem
												{
													Id = od.Id,
													Note = od.Note,
													OrderId = od.OrderId,
													Quantity = od.Quantity,
													OrderProduct = new ProductDto()
													{
														Id = p.Id,
														Description = p.Description,
														FavoriteCount = p.FavoriteCount,
														Image = p.Image,
														IsAvailable = p.IsAvailable,
														Name = p.Name,
														UnitPrice = p.UnitPrice,
														Merchant = new MerchantDto()
														{
															Id = mer.Id,
															PhoneNumber = mer.PhoneNumber,
															Avatar = mer.Avatar,
															User = queryUser.SingleOrDefault(e => e.Id == mer.UserId),
															Name = mer.Name,
															OpenTime = CustomTime.GetTime(mer.OpenTime),
															CloseTime = CustomTime.GetTime(mer.CloseTime),
															HouseNumber = mer.HouseNumber,
															City = queryCity.SingleOrDefault(e => e.CityCode == mer.CityCode),
															District = queryDistrict.SingleOrDefault(e => e.DistrictCode == mer.DistrictCode),
															Ward = queryWard.SingleOrDefault(e => e.WardCode == mer.WardCode),
														},
														OrderCount = p.OrderCount,
														MenuItem = new MenuItemDto()
														{
															Id = mi.Id,
															Name = mi.Name,
															Description = mi.Description,
															MerchantId = mi.MerchantId
														},
														ToppingList = (from t in _dbContext.Toppings
																			where t.ProductId == p.Id
																			select new ToppingDto()
																			{
																				Id = t.Id,
																				Name = t.Name,
																				ProductId = t.ProductId,
																				UnitPrice = t.UnitPrice
																			}).ToList()
													},
													SelectedToppingList = (from odt in _dbContext.OrderDetailToppings
																				  where odt.OrderDetailId == od.Id
																				  join t in _dbContext.Toppings
																					  on odt.ToppingId equals t.Id into ts
																				  from t in ts.DefaultIfEmpty()
																				  select new ToppingDto()
																				  {
																					  Id = t.Id,
																					  Name = t.Name,
																					  ProductId = t.ProductId,
																					  UnitPrice = t.UnitPrice
																				  }).ToList()
												};

					var queryResult = (from order in _dbContext.Orders
												 //where order.Status == (byte)request._orderStatus
											 select new OrderDto
											 {
												 Id = order.Id,
												 DeliveryDateTime = order.DeliveryDateTime,
												 OrderDateTime = order.OrderDateTime,
												 Note = order.Note,
												 ReceiveVia = order.ReceiveVia,
												 Status = (OrderStatus)order.Status,
												 Shipper = queryUser.SingleOrDefault(e => e.Id == order.ShipperId),
												 User = queryUser.SingleOrDefault(e => e.Id == order.UserId),
												 ShippingAddress = queryShippingAddress.SingleOrDefault(s => s.Id == order.ShippingAddressId),
												 OrderItemList = queryOrderItem.Where(o => o.OrderId == order.Id).ToList()
											 });
					if (string.IsNullOrWhiteSpace(request._userId))
					{
						if (request._orderStatus == OrderStatus.None)
						{
							result = await queryResult.ToListAsync();
						}
						else
						{
							result = await queryResult.Where(e => e.Status == request._orderStatus).ToListAsync();
						}
					}
					else
					{
						if (request._orderStatus == OrderStatus.None)
						{
							result = await queryResult.Where(e => e.User.Id == request._userId).ToListAsync();
						}
						else
						{
							result = await queryResult
								.Where(e => e.Status == request._orderStatus && e.User.Id == request._userId)
								.ToListAsync();
						}
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, ex.Message);
					throw;
				}
				return result;
			}
		}
	}
}
