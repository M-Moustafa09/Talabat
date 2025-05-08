using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Repository.Dbcontext;
using Talapat.Core.Entities;

namespace Talabat.Repository.Data
{
    public static class StorContextSeed
    {
        public static async Task SeedAsynk(StorDbcontrext storDbcontrext)
        {
            if (!storDbcontrext.ProductBrands.Any()) 
            {
                #region seeding Brands
                var BrandData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);

                if (brands?.Count > 0)
                {
                    foreach (var item in brands)

                    {
                        await storDbcontrext.Set<ProductBrand>().AddAsync(item);
                    }
                    await storDbcontrext.SaveChangesAsync();

                }
                #endregion

            }
            if (!storDbcontrext.ProductTypes.Any())
            {
                #region Seeding Typs
                var TypeData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

                if (Types?.Count > 0)
                {
                    foreach (var item in Types)

                    {
                        await storDbcontrext.Set<ProductType>().AddAsync(item);
                    }
                    await storDbcontrext.SaveChangesAsync();

                }
                #endregion
            }

            if (!storDbcontrext.products.Any())
            {

            #region Seeding Product
            var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
            var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);

            if (Products?.Count > 0)
            {
                foreach (var item in Products)

                {
                    await storDbcontrext.Set<Product>().AddAsync(item);
                }
                await storDbcontrext.SaveChangesAsync();

            }

            #endregion

            }

        }

    }
}
