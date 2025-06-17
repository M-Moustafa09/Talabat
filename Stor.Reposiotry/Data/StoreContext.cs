using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;
using Store.Core.Order_Aggragate;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Stor.Reposiotry.Data
{
	public class StoreContext :DbContext
	{
		public StoreContext(DbContextOptions<StoreContext> options) : base(options)
		
		{ 
		
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(modelBuilder);
		}   
		public DbSet<Product> produts { get; set; }
		public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<ProductType> productTypes { get; set; }
		public DbSet<Order> orders { get; set; }
		public DbSet<OrderItem> orderItems { get; set; }
		public DbSet<DeliveryMethod> deliveryMethods { get; set; }


    }
}
