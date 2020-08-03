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
	public class GetAllFeaturedDishCategoryQuery : IRequest<IEnumerable<FeaturedDishDto>>
	{
		public class Handler : IRequestHandler<GetAllFeaturedDishCategoryQuery, IEnumerable<FeaturedDishDto>>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<GetAllFeaturedDishCategoryQuery> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<GetAllFeaturedDishCategoryQuery> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<IEnumerable<FeaturedDishDto>> Handle(
				GetAllFeaturedDishCategoryQuery request, CancellationToken cancellationToken)
			{
				var result = new List<FeaturedDishDto>();
				try
				{
					result = await (from fe in _dbContext.FeaturedDishCategoies
										 select new FeaturedDishDto
										 {
											 Id = fe.Id,
											 Name = fe.Name,
										 }).ToListAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);
					return null;
				}
				return result;
			}
		}
	}
}
