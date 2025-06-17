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
	public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder.Property(OI => OI.Price).HasColumnType("decimal(18,2)");

			builder.OwnsOne(OI => OI.product, P => P.WithOwner());
		}
	}
}
