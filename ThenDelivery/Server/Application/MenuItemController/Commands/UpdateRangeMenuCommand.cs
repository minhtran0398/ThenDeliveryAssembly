using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Server.Persistence;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Application.MenuItemController.Commands
{
	public class UpdateRangeMenuCommand : IRequest<Unit>
	{
		private readonly IEnumerable<MenuItemDto> _menuList;

		public UpdateRangeMenuCommand(IEnumerable<MenuItemDto> menuList)
		{
			_menuList = menuList ?? throw new ArgumentNullException(nameof(menuList));
		}

		public class Handler : IRequestHandler<UpdateRangeMenuCommand, Unit>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<UpdateRangeMenuCommand> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<UpdateRangeMenuCommand> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<Unit> Handle(UpdateRangeMenuCommand request, CancellationToken cancellationToken)
			{
				using var trans = _dbContext.Database.BeginTransaction();
				try
				{
					_dbContext.MenuItems.UpdateRange(GetData(request._menuList));
					await _dbContext.SaveChangesAsync();
					await trans.CommitAsync();
				}
				catch (Exception ex)
				{
					await trans.RollbackAsync();
					_logger.LogError(ex, ex.Message);
					throw;
				}
				return Unit.Value;
			}

			private IEnumerable<MenuItem> GetData(IEnumerable<MenuItemDto> menuDtoList)
			{
				foreach (var menuDto in menuDtoList)
				{
					yield return new MenuItem()
					{
						Id = menuDto.Id,
						MerchantId = menuDto.MerchantId,
						Name = menuDto.Name,
						Description = menuDto.Description
					};
				}
			}
		}
	}
}
