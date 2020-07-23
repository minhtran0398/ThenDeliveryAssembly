using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace ThenDelivery.Client.Components
{
	public class CustomComponentBase<T> : ComponentBase
	{
		[Inject] public ILogger<T> Logger { get; set; }
		[Inject] public HttpClient HttpClient { get; set; }
		[Inject] public NavigationManager NavigationManager { get; set; }

		public string BaseUrl { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();
			BaseUrl = NavigationManager.BaseUri;
		}
	}
}
