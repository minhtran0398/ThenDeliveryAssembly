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

namespace ThenDelivery.Server.Application.CityController.Queries
{
	public class GetAllCityQuery : IRequest<IEnumerable<CityDto>>
	{
		public class Handler : IRequestHandler<GetAllCityQuery, IEnumerable<CityDto>>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<GetAllCityQuery> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<GetAllCityQuery> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<IEnumerable<CityDto>> Handle(GetAllCityQuery request, CancellationToken cancellationToken)
			{
				var result = new List<CityDto>();
				try
				{
					result = await (from city in _dbContext.Cities
										 join level in _dbContext.CityLevels
											 on city.CityLevelId equals level.Id
										 select new CityDto
										 {
											 CityCode = city.CityCode,
											 Name = city.Name,
											 CityLevelId = level.Id,
											 CityLevelName = level.Name
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
