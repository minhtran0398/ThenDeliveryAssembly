using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Security.Claims;

namespace ThenDelivery.Client.Components
{
	public class CustomComponentBase<T> : ComponentBase
	{
		[Inject] public ILogger<T> Logger { get; set; }
		[Inject] IHttpClientFactory HttpClientFactory { get; set; }
		[Inject] public HttpClient HttpClientServer { get; set; }
		[Inject] public HttpClient HttpClientAnonymous { get; set; }
		[Inject] public NavigationManager NavigationManager { get; set; }

		public string BaseUrl { get; set; }
		public ClaimsPrincipal User { get; set; }

		protected override void OnInitialized()
		{
			BaseUrl = NavigationManager.BaseUri;
			HttpClientServer = HttpClientFactory.CreateClient("ThenDelivery.ServerAPI");
			HttpClientAnonymous = HttpClientFactory.CreateClient("ThenDelivery.AnonymousAPI");
			base.OnInitialized();
		}
	}
}
