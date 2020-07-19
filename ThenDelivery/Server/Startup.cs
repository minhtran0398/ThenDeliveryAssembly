using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using ThenDelivery.Server.Application.Common.Interfaces;
using ThenDelivery.Server.Data;
using ThenDelivery.Server.Services;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ThenDeliveryDbContext>(options =>
				 options.UseSqlServer(
					  Configuration.GetConnectionString("DefaultConnection")));
			services.AddDefaultIdentity<User>(options =>
			{
				options.SignIn.RequireConfirmedAccount = true;
				options.Password.RequireDigit = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
			}).AddRoles<IdentityRole>().AddEntityFrameworkStores<ThenDeliveryDbContext>();

			// Configure identity server to put the role claim into the id token 
			// and the access token and prevent the default mapping for roles 
			// in the JwtSecurityTokenHandler.
			services
				.AddIdentityServer()
				.AddApiAuthorization<User, ThenDeliveryDbContext>(options => {
					options.IdentityResources["openid"].UserClaims.Add("role");
					options.ApiResources.Single().UserClaims.Add("role");
				});
			services.AddAuthentication().AddIdentityServerJwt();
			// Need to do this as it maps "role" to ClaimTypes.Role and causes issues
			System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler
				 .DefaultInboundClaimTypeMap.Remove("role");

			#region additional services
			services.AddTransient<ICurrentUserService, CurrentUserService>();
			services.AddTransient<IEmailSender, EmailSender>(i =>
				new EmailSender(
					Configuration["EmailSender:Host"],
					Configuration.GetValue<int>("EmailSender:Port"),
					Configuration.GetValue<bool>("EmailSender:EnableSSL"),
					Configuration["EmailSender:UserName"],
					Configuration["EmailSender:Password"]
				)
			);
			#endregion

			#region default
			services.AddControllersWithViews();
			services.AddRazorPages();
			#endregion
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseIdentityServer();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}
