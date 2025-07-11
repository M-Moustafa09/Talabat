﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stor.Reposiotry.Data.Configurations
{
	public class ProductConfigruation : IEntityTypeConfiguration<Product>

	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasOne(P => P.ProductBrand)
				.WithMany()
				.HasForeignKey(p => p.ProductBrandId);

				builder.HasOne(P=>P.ProductType)
				.WithMany()
				.HasForeignKey(p => p.ProductTypeId);
			builder.Property(P=>P.Name).IsRequired();
			builder.Property(P=>P.PictureUrl).IsRequired();
			builder.Property(P=> P.Description).IsRequired();
			builder.Property(P=> P.Price).HasColumnType("decimal(18,2)");








		}
	}
}
