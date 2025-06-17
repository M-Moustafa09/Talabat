using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.Order_Aggragate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stor.Reposiotry.Data.Configurations
{
	public class OrderConfig : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.Property(o=>o.Status)
				.HasConversion(OStatus=>OStatus.ToString(),OStatus=>(OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus));
			builder.Property(o => o.SubTotal)
				.HasColumnType("decimal(18,2)");
			builder.OwnsOne(o => o.ShaippingAddress, X => X.WithOwner());
			builder.HasOne(O => O.DeliveryMethod)
			.WithMany()
			.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
