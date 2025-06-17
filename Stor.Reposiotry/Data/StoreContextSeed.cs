using Store.Core.Entities;
using Store.Core.Order_Aggragate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Stor.Reposiotry.Data
{
	public static class StoreContextSeed
	{
		public static async Task SeedAsync(StoreContext Dbcontext)
		{
			//seed Product Brand 
			if (!Dbcontext.productBrands.Any())
			{
				var BrandData = File.ReadAllText("../Stor.Reposiotry/Data/DataSeed/brands.json");
				var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
				if (Brands?.Count() > 0)
				{
					foreach (var Brand in Brands)
					{
						await Dbcontext.Set<ProductBrand>().AddAsync(Brand);
					}
					await Dbcontext.SaveChangesAsync();
				}

			}
			//seed Product Type
			if (!Dbcontext.productTypes.Any())
			{
				var TypeData = File.ReadAllText("../Stor.Reposiotry/Data/DataSeed/types.json");
				var types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
				if (types?.Count() > 0)
				{
					foreach (var type in types)
					{
						await Dbcontext.Set<ProductType>().AddAsync(type);
					}
					await Dbcontext.SaveChangesAsync();
				}

			}
			// Seed Product 
			if (!Dbcontext.produts.Any())
			{
				var ProductData = File.ReadAllText("../Stor.Reposiotry/Data/DataSeed/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(ProductData);
				if (products?.Count() > 0)
				{
					foreach (var product in products)
					{
						await Dbcontext.Set<Product>().AddAsync(product);
					}
					await Dbcontext.SaveChangesAsync();
				}

			}

			if (!Dbcontext.deliveryMethods.Any())
			{
				var deliverydata = File.ReadAllText("../Stor.Reposiotry/Data/DataSeed/delivery.json");
				var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliverydata);
				if (DeliveryMethods?.Count() > 0)
				{
					foreach (var deliveryMethod in DeliveryMethods)
					{
						await Dbcontext.Set<DeliveryMethod>().AddAsync(deliveryMethod);
					}
					await Dbcontext.SaveChangesAsync();
				}

			}


		}


	}
}
