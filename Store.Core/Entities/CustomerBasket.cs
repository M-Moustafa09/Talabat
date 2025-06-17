using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities
{
	public class CustomerBasket
	{
		public string Id { get; set; }
        public string  PaymentIntentId { get; set; }
        public string   ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public List<BasktItem> Items { get; set;} = new List<BasktItem>();

        public CustomerBasket( string id )
        {
            Id = id;
        }
    }
}
