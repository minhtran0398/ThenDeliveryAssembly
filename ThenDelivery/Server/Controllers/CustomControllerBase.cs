using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ThenDelivery.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomControllerBase<T> : ControllerBase
	{
		private IMediator _mediator;
		private ILogger<T> _logger;

		protected IMediator Mediator 
			=> _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
		protected ILogger<T> Logger
			=> _logger ??= HttpContext.RequestServices.GetService<ILogger<T>>();

	}
}
