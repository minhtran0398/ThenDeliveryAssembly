using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using ThenDelivery.Server.Data;
using ThenDelivery.Server.Services;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Infrastructure
{
   public static class ServiceExtensions
   {
      public static void ConfigureIdentity(this IServiceCollection services)
      {
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
               options.IdentityResources["openid"].UserClaims.Add("name");
               options.ApiResources.Single().UserClaims.Add("name");
               options.IdentityResources["openid"].UserClaims.Add("role");
               options.ApiResources.Single().UserClaims.Add("role");
            });
         services.AddAuthentication().AddIdentityServerJwt();
         // Need to do this as it maps "role" to ClaimTypes.Role and causes issues
         System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler
             .DefaultInboundClaimTypeMap.Remove("role");

         services.Configure<IdentityOptions>(options =>
            options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);
      }

      public static void ConfigureDatabase(this IServiceCollection services,
         IConfiguration configuration)
      {
         services.AddDbContext<ThenDeliveryDbContext>(options =>
         {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            options.UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddDebug(); }));
         });
      }

      public static void ConfigureCors(this IServiceCollection services)
      {
         services.AddCors(options =>
         {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
         });
      }

      public static void ConfigureEmail(this IServiceCollection services,
         IConfiguration configuration)
      {
         services.AddTransient<IEmailSender, EmailSender>(i =>
            new EmailSender(
               configuration["EmailSender:Host"],
               configuration.GetValue<int>("EmailSender:Port"),
               configuration.GetValue<bool>("EmailSender:EnableSSL"),
               configuration["EmailSender:UserName"],
               configuration["EmailSender:Password"]
            )
         );
      }
   }
}
