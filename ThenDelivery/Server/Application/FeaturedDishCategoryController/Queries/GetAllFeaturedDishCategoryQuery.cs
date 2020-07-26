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
	public class GetAllFeaturedDishCategoryQuery : IRequest<IEnumerable<FeaturedDishCategoryDto>>
	{
		public class Handler : IRequestHandler<GetAllFeaturedDishCategoryQuery, IEnumerable<FeaturedDishCategoryDto>>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<GetAllFeaturedDishCategoryQuery> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<GetAllFeaturedDishCategoryQuery> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<IEnumerable<FeaturedDishCategoryDto>> Handle(
				GetAllFeaturedDishCategoryQuery request, CancellationToken cancellationToken)
			{
				var result = new List<FeaturedDishCategoryDto>();
				try
				{
					result = await (from fe in _dbContext.FeaturedDishCategoies
										 select new FeaturedDishCategoryDto
										 {
											 FeaturedDishCategoryId = fe.FeaturedDishCategoryId,
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
