using Store.Core.Order_Aggragate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications.OrderSpec
{
	public class OrderSpecs :BaseSpecification<Order>
	{
		public OrderSpecs(string email):base(o=> o.BuyerEmail==email)
		{
			includs.Add(o => o.DeliveryMethod);
			includs.Add(o => o.Items);
			SetOrderByDece(o => o.OrderDate);


		}
        public OrderSpecs(string email,int orderid):base(o=>o.BuyerEmail==email&&o.Id== orderid)
        {
			includs.Add(o => o.DeliveryMethod);
			includs.Add(o => o.Items);
		}
    }
}
