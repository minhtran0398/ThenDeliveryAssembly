using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using ThenDelivery.Shared.Validation;
using FluentValidation;
using Microsoft.JSInterop;

namespace ThenDelivery.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

			builder.Services.AddHttpClient("ThenDelivery.AnonymousAPI", client =>
			{
				client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
			});

			// auto inject this HttpClient
			builder.Services.AddHttpClient("ThenDelivery.ServerAPI",
				client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
				 .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

			// Supply HttpClient instances that include access tokens when making requests to the server project
			// builder.Services
			// 	.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>()
			// 		.CreateClient("ThenDelivery.ServerAPI"))
			// 	.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>()
			// 		.CreateClient("ThenDelivery.AnonymousAPI"));
			// builder.Services
			// 	.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>()
			// 	.CreateClient("ThenDelivery.AnonymousAPI"));

			builder.Services.AddDevExpressBlazor();
			builder.Services.AddValidatorsFromAssemblyContaining<MerchantValidator>();

			builder.Services
				.AddApiAuthorization()
				.AddAccountClaimsPrincipalFactory<CustomUserFactory>();

			await builder.Build().RunAsync();
		}
	}
}
