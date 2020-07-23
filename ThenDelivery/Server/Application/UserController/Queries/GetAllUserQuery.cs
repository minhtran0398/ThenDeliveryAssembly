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

namespace ThenDelivery.Server.Application.UserController.Queries
{
    public class GetAllUserQuery : IRequest<IEnumerable<UserDto>>
    {
        public class Handler : IRequestHandler<GetAllUserQuery, IEnumerable<UserDto>>
        {
            private readonly ThenDeliveryDbContext _dbContext;
            private readonly ILogger<GetAllUserQuery> _logger;

            public Handler(ThenDeliveryDbContext dbContext, ILogger<GetAllUserQuery> logger)
            {
                _dbContext = dbContext;
                _logger = logger;
            }

            public async Task<IEnumerable<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
            {
                var result = new List<UserDto>();
                try
                {
                    result = await (from u in _dbContext.Users
                                    let query = (from ur in _dbContext.UserRoles
                                                 where ur.UserId.Equals(u.Id)
                                                 join r in _dbContext.Roles on ur.RoleId equals r.Id
                                                 select new RoleDto()
                                                 {
                                                     RoleId = r.Id,
                                                     RoleName = r.Name
                                                 })
                                    select new UserDto
                                    {
                                        UserId = u.Id,
                                        UserName = u.UserName,
                                        Email = u.Email,
                                        BirthDate = u.BirthDate,
                                        IsEmailConfirmed = u.EmailConfirmed,
                                        IsPhoneNumberConfirmed = u.PhoneNumberConfirmed,
                                        PhoneNumber = u.PhoneNumber,
                                        RoleList = query.ToList()
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
