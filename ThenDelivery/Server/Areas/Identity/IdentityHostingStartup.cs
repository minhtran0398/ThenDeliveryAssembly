using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ThenDelivery.Server.Areas.Identity.IdentityHostingStartup))]
namespace ThenDelivery.Server.Areas.Identity
{
	public class IdentityHostingStartup : IHostingStartup
	{
		public void Configure(IWebHostBuilder builder)
		{
			builder.ConfigureServices((context, services) =>
			{
			});
		}
	}
}