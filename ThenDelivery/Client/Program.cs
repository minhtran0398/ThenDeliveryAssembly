using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Hosting;

namespace ThenDelivery.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");
			builder.Services.AddHttpClient("ThenDelivery.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
				 .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

			// Supply HttpClient instances that include access tokens when making requests to the server project
			builder.Services
				.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>()
				.CreateClient("ThenDelivery.ServerAPI"));

			builder.Services.AddDevExpressBlazor();

			builder.Services
				.AddApiAuthorization()
				.AddAccountClaimsPrincipalFactory<CustomUserFactory>();

			await builder.Build().RunAsync();
		}
	}
}
