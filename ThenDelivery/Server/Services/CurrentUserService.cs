using Microsoft.AspNetCore.Http;
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

	}
}
