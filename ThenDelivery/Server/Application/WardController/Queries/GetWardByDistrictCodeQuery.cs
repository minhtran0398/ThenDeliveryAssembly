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
	public class GetWardByDistrictCodeQuery : IRequest<IEnumerable<WardDto>>
	{
		private readonly string _districtCode;

		public GetWardByDistrictCodeQuery(string districtCode)
		{
			_districtCode = districtCode;
		}

		public class Handler : IRequestHandler<GetWardByDistrictCodeQuery, IEnumerable<WardDto>>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<GetWardByDistrictCodeQuery> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<GetWardByDistrictCodeQuery> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<IEnumerable<WardDto>> Handle(GetWardByDistrictCodeQuery request, CancellationToken cancellationToken)
			{
				var result = new List<WardDto>();
				try
				{
					result = await (from ward in _dbContext.Wards
										 where ward.DistrictCode == request._districtCode
										 join level in _dbContext.WardLevels
											 on ward.WardLevelId equals level.WardLevelId
										 select new WardDto
										 {
											 WardCode = ward.WardCode,
											 Name = ward.Name,
											 DistrictCode = ward.DistrictCode,
											 WardLevelId = level.WardLevelId,
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
