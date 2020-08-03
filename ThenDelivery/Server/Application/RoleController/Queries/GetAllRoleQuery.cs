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

namespace ThenDelivery.Server.Application.RoleController.Queries
{
    public class GetAllRoleQuery : IRequest<IEnumerable<RoleDto>>
    {
        public class Handler : IRequestHandler<GetAllRoleQuery, IEnumerable<RoleDto>>
        {
            private readonly ThenDeliveryDbContext _dbContext;
            private readonly ILogger<GetAllRoleQuery> _logger;

            public Handler(ThenDeliveryDbContext dbContext, ILogger<GetAllRoleQuery> logger)
            {
                _dbContext = dbContext;
                _logger = logger;
            }

            public async Task<IEnumerable<RoleDto>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
            {
                var result = new List<RoleDto>();
                try
                {
                    result = await _dbContext.Roles.Select(r => new RoleDto()
                    {
                        Id = r.Id,
                        Name = r.Name
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
