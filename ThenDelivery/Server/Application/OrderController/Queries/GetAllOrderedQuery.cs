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

namespace ThenDelivery.Server.Application.OrderController.Queries
{
	public class GetAllOrderedQuery : IRequest<IEnumerable<OrderDto>>
	{
		public class Handler : IRequestHandler<GetAllOrderedQuery, IEnumerable<OrderDto>>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<GetAllOrderedQuery> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<GetAllOrderedQuery> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<IEnumerable<OrderDto>> Handle(GetAllOrderedQuery request,
				CancellationToken cancellationToken)
			{
				var result = new List<OrderDto>();
				try
				{
					var queryUser = (from u in _dbContext.Users
										  select new UserDto
										  {
											  Id = u.Id,
											  UserName = u.UserName,
											  //Email = u.Email,
											  //BirthDate = u.BirthDate,
											  //IsEmailConfirmed = u.EmailConfirmed,
											  //IsPhoneNumberConfirmed = u.PhoneNumberConfirmed,
											  PhoneNumber = u.PhoneNumber,
											  RoleList = null
										  });
					var queryShippingAddress = from address in _dbContext.ShippingAddresses
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


					result = await (from order in _dbContext.Orders
										 where order.Status == 1
										 select new OrderDto
										 {
											 Id = order.Id,
											 DeliveryDateTime = order.DeliveryDateTime,
											 OrderDateTime = order.OrderDateTime,
											 Note = order.Note,
											 ReceiveVia = order.ReceiveVia,
											 Status = order.Status,
											 Shipper = queryUser.SingleOrDefault(e => e.Id == order.ShipperId),
											 User = queryUser.SingleOrDefault(e => e.Id == order.UserId),
											 ShippingAddress = queryShippingAddress.SingleOrDefault(s => s.Id == order.ShippingAddressId),
											 OrderItemList = queryOrderItem.Where(o => o.OrderId == order.Id).ToList()
										 }).ToListAsync();
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
