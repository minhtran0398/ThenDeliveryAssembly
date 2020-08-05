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

namespace ThenDelivery.Server.Application.MerchantMenuController.Commands
{
	public class InsertRangeMenuItemCommand : IRequest<Unit>
	{
		private readonly IEnumerable<MenuItemDto> _menuList;

		public InsertRangeMenuItemCommand(IEnumerable<MenuItemDto> menuList)
		{
			_menuList = menuList ?? throw new ArgumentNullException(nameof(menuList));
		}

		public class Handler : IRequestHandler<InsertRangeMenuItemCommand, Unit>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<InsertRangeMenuItemCommand> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<InsertRangeMenuItemCommand> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<Unit> Handle(InsertRangeMenuItemCommand request, CancellationToken cancellationToken)
			{
				using var trans = _dbContext.Database.BeginTransaction();
				try
				{
					await _dbContext.MenuItems.AddRangeAsync(GetData(request._menuList));
					await _dbContext.SaveChangesAsync();
					await trans.CommitAsync();
				}
				catch (DbUpdateConcurrencyException ex)
				{
					await trans.RollbackAsync();
					_logger.LogError(ex, ex.Message);
					throw new DbUpdateConcurrencyException
						("[MenuItems] A concurrency violation is encountered while saving to the database");
				}
				catch (DbUpdateException ex)
				{
					await trans.RollbackAsync();
					_logger.LogError(ex, ex.Message);
					throw new DbUpdateException
						("[MenuItems] An error is encountered while saving to the database");
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
						Id = 0,
						MerchantId = menuDto.MerchantId,
						Name = menuDto.Name,
						Description = menuDto.Description
					};
				}
			}
		}
	}
}
