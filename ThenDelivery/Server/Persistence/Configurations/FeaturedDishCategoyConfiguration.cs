﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Persistence.Configurations
{
	public class FeaturedDishCategoyConfiguration : IEntityTypeConfiguration<FeaturedDishCategory>
	{
		public void Configure(EntityTypeBuilder<FeaturedDishCategory> builder)
		{
			builder.ToTable("FeaturedDishCategoies");

			builder.HasKey(e => e.FeaturedDishCategoryId);
			builder.HasIndex(e => e.Name).IsUnique(true);

			builder.Property(e => e.Name)
					.HasColumnName("Name")
					.HasMaxLength(64)
					.IsRequired(true);
		}
	}
}
