using Store.Core.Order_Aggragate;

namespace Store.APIs.DTOs
{
	public class OrderDto
	{
        public string  BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto   ShippingAddress { get; set; }
    }
}
