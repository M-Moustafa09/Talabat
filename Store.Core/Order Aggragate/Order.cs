using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Order_Aggragate
{
    public class Order : BaseClass
    {
        public Order() { }
		public Order(string buyerEmail, Address shaippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal,string paymentintentid)
		{
			BuyerEmail = buyerEmail;
			ShaippingAddress = shaippingAddress;
			DeliveryMethod = deliveryMethod;
			Items = items;
			SubTotal = subTotal;
            PaymentIntentID = paymentintentid;
		}

		public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShaippingAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public string PaymentIntentID { get; set; }   
        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Cost;
        }






    }
}
