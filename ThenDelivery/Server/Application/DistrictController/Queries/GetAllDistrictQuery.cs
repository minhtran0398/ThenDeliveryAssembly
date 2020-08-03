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

namespace ThenDelivery.Server.Application.DistrictController.Queries
{
	public class GetAllDistrictQuery : IRequest<IEnumerable<DistrictDto>>
	{
		public class Handler : IRequestHandler<GetAllDistrictQuery, IEnumerable<DistrictDto>>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<GetAllDistrictQuery> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<GetAllDistrictQuery> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<IEnumerable<DistrictDto>> Handle(GetAllDistrictQuery request, CancellationToken cancellationToken)
			{
				var result = new List<DistrictDto>();
				try
				{
					result = await (from district in _dbContext.Districts
										 join level in _dbContext.DistrictLevels
											 on district.DistrictLevelId equals level.Id
										 select new DistrictDto
										 {
											 DistrictCode = district.DistrictCode,
											 Name = district.Name,
											 CityCode = district.CityCode,
											 DistrictLevelId = level.Id,
											 DistrictLevelName = level.Name
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