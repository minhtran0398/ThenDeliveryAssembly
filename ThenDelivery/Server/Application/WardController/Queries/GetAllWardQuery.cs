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

namespace ThenDelivery.Server.Application.WardController.Queries
{
	public class GetAllWardQuery : IRequest<IEnumerable<WardDto>>
	{
		public class Handler : IRequestHandler<GetAllWardQuery, IEnumerable<WardDto>>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<GetAllWardQuery> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<GetAllWardQuery> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<IEnumerable<WardDto>> Handle(GetAllWardQuery request, CancellationToken cancellationToken)
			{
				var result = new List<WardDto>();
				try
				{
					result = await (from ward in _dbContext.Wards
										 join level in _dbContext.WardLevels
											 on ward.WardLevelId equals level.Id
										 select new WardDto
										 {
											 WardCode = ward.WardCode,
											 Name = ward.Name,
											 DistrictCode = ward.DistrictCode,
											 WardLevelId = level.Id,
											 WardLevelName = level.Name
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
