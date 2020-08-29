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

namespace ThenDelivery.Server.Application.FeaturedDishCategoryController.Queries
{
	public class GetAllFeaturedDishQuery : IRequest<IEnumerable<FeaturedDishDto>>
	{
		private readonly int _id;

		public GetAllFeaturedDishQuery(int id = 0)
		{
			_id = id;
		}

		public class Handler : IRequestHandler<GetAllFeaturedDishQuery, IEnumerable<FeaturedDishDto>>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<GetAllFeaturedDishQuery> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<GetAllFeaturedDishQuery> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<IEnumerable<FeaturedDishDto>> Handle(
				GetAllFeaturedDishQuery request, CancellationToken cancellationToken)
			{
				var result = new List<FeaturedDishDto>();
				try
				{
					if (request._id == 0)
					{
						result = await (from fe in _dbContext.FeaturedDishes
											 select new FeaturedDishDto
											 {
												 Id = fe.Id,
												 Name = fe.Name,
											 }).ToListAsync();
					}
					else
					{
						result = await (from fe in _dbContext.FeaturedDishes
											 where fe.Id == request._id
											 select new FeaturedDishDto
											 {
												 Id = fe.Id,
												 Name = fe.Name,
											 }).ToListAsync();
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
