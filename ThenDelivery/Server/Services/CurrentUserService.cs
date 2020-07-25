using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using ThenDelivery.Server.Application.Common.Interfaces;

namespace ThenDelivery.Server.Services
{
	public class CurrentUserService : ICurrentUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string UserName
			=> _httpContextAccessor.HttpContext?.User?.Identity?.Name;

		public string GetLoggedInUserId()
		{
			var principal = _httpContextAccessor.HttpContext.User;
			if (principal == null)
				throw new ArgumentNullException(nameof(principal));

			var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

			return loggedInUserId;
		}
	}
}
