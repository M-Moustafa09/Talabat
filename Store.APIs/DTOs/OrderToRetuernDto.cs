using Store.Core.Order_Aggragate;
using System.Security.Principal;

namespace Store.APIs.DTOs
{
	public class OrderToRetuernDto
	{
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
		public DateTimeOffset OrderDate { get; set; } 
		public string Status { get; set; }
		public Address ShaippingAddress { get; set; }

        public string DeliveryMethod { get; set; }// Name 
        public decimal DeliveryMethodCost { get; set; }
		public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
		public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentID { get; set; } 
	}
}
