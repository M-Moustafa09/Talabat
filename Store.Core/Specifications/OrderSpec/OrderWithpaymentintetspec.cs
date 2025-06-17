using Store.Core.Order_Aggragate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Specifications.OrderSpec
{
	public class OrderWithpaymentintetspec:BaseSpecification<Order>
	{
        public OrderWithpaymentintetspec(string paymentintentid ):base(o=>o.PaymentIntentID== paymentintentid)
        {
            
        }

    }
}
