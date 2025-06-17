using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities
{
	public class Product :BaseClass
	{
       
		public string Name { get; set; }
		public string Description { get; set; }
        public string PictureUrl { get; set; }//PictureUrl
		public decimal Price { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductBrandId { get; set; }//FK

        public ProductType ProductType { get; set; }//Navigational Property
        public int ProductTypeId { get; set; }  //Fk







    }
}
