using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Server.Application.Common.Interfaces;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence
{
	public class ThenDeliveryDbContext : ApiAuthorizationDbContext<User>
	{
		private readonly ICurrentUserService _currentUserService;

		public ThenDeliveryDbContext(
			 DbContextOptions<ThenDeliveryDbContext> options,
			 IOptions<OperationalStoreOptions> operationalStoreOptions,
			 ICurrentUserService currentUserService)
			 : base(options, operationalStoreOptions)
		{
			_currentUserService = currentUserService;
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var currentTime = DateTime.Now;
			foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedBy = _currentUserService.UserName;
						entry.Entity.Created = currentTime;
						break;
					case EntityState.Modified:
						entry.Entity.LastModifiedBy = _currentUserService.UserName;
						entry.Entity.LastModified = currentTime;
						break;
					case EntityState.Deleted:
						entry.Entity.IsDeleted = true;
						break;
				}
			}
			return base.SaveChangesAsync(cancellationToken);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ThenDeliveryDbContext).Assembly);
			modelBuilder.Entity<User>().ToTable("Users");
			modelBuilder.Entity<IdentityRole>().ToTable("Roles");
			modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
			modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
			modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
			modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
			modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");

			modelBuilder.Entity<IdentityRole>().HasData(new List<IdentityRole>
				{
					 new IdentityRole(Const.Role.UserRole),
					 new IdentityRole(Const.Role.ShipperRole),
					 new IdentityRole(Const.Role.MerchantRole),
					 new IdentityRole(Const.Role.AdministrationRole),
				});
		}

		public DbSet<City> Cities { get; set; }
		public DbSet<CityLevel> CityLevels { get; set; }
		public DbSet<District> Districts { get; set; }
		public DbSet<DistrictLevel> DistrictLevels { get; set; }
		public DbSet<FeaturedDishCategory> FeaturedDishCategoies { get; set; }
		public DbSet<Merchant> Merchants { get; set; }
		public DbSet<MerchantType> MerchantTypes { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ShippingAddress> ShippingAddresses { get; set; }
		public DbSet<MerchantMenu> MerchantMenues { get; set; }
		public DbSet<Topping> Toppings { get; set; }
		public DbSet<Ward> Wards { get; set; }
		public DbSet<WardLevel> WardLevels { get; set; }
		public DbSet<MerchantTypeMerchant> MerchantTypeMerchants { get; set; }
		public DbSet<FeaturedDishCategoryMerchant> FeaturedDishCategoryMerchants { get; set; }
	}
}
