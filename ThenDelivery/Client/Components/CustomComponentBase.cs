using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ThenDelivery.Client.Components
{
	public class CustomComponentBase<T> : ComponentBase
	{
		[Inject] public ILogger<T> Logger { get; set; }
		[Inject] IHttpClientFactory HttpClientFactory { get; set; }
		public HttpClient HttpClientServer { get; set; }
		public HttpClient HttpClientAnonymous { get; set; }
		[Inject] public NavigationManager NavigationManager { get; set; }
		[Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

		public string BaseUrl { get; set; }
		public ClaimsPrincipal User { get; set; }

		protected override void OnInitialized()
		{
			BaseUrl = NavigationManager.BaseUri;
			HttpClientServer = HttpClientFactory.CreateClient("ThenDelivery.ServerAPI");
			HttpClientAnonymous = HttpClientFactory.CreateClient("ThenDelivery.AnonymousAPI");
			base.OnInitialized();
		}

		protected override async Task OnInitializedAsync()
		{
			var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
			User = authState.User;
		}

		protected bool IsAuthenticated()
		{
			return User.Identity.IsAuthenticated;
		}

		protected void NavigateToLogin()
		{
			NavigationManager.NavigateTo($"authentication/login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}");
		}
	}
}
