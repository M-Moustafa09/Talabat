using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications
{
	public class producrtprandAndProductTypeSpec:BaseSpecification<Product>
	{
        public producrtprandAndProductTypeSpec(ProductSpecPram productSpecPram) :
            base(p=>
            (string.IsNullOrEmpty(productSpecPram.Search)||p.Name.ToLower().Contains(productSpecPram.Search))
            &&
            (!productSpecPram.BrandId.HasValue || p.ProductBrandId== productSpecPram.BrandId)
           &&
            (!productSpecPram.TypeId.HasValue || p.ProductTypeId== productSpecPram.TypeId)
                  
                )
        {
            includs.Add(p => p.ProductBrand);
            includs.Add(P => P.ProductType);
            if(!string.IsNullOrEmpty(productSpecPram.Sort))
            {
                switch (productSpecPram.Sort)
                {
                    case "PriceAce":
                        SetOrderBy(p => p.Price);
                            break;
                    case "PriceDece":
                        SetOrderByDece(p => p.Price);
                        break;
                        default:
                        SetOrderBy(p => p.Name);
                        break;

				}
            }
            SetSKipTakeValue(productSpecPram.PageSize * (productSpecPram.PageIndex - 1), productSpecPram.PageSize);


        }
        public producrtprandAndProductTypeSpec(int id) : base(P=>P.Id==id)
        {
            includs.Add(p => p.ProductBrand);
            includs.Add(P => P.ProductType);

        }
    }
}
