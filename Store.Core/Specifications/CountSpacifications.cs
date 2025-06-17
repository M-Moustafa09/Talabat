using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications
{
	public class CountSpacifications :BaseSpecification<Product>
	{
		public CountSpacifications(ProductSpecPram productSpecPram) : base(p =>
		(string.IsNullOrEmpty(productSpecPram.Search) || p.Name.ToLower().Contains(productSpecPram.Search))
			&&
			(!productSpecPram.BrandId.HasValue || p.ProductBrandId == productSpecPram.BrandId)
		   &&
			(!productSpecPram.TypeId.HasValue || p.ProductTypeId == productSpecPram.TypeId)

				)
		{ }

	}
}
