using System.ComponentModel.DataAnnotations;

namespace Store.APIs.DTOs
{
	public class CustomerBasketDto 
	{
		[Required]
        public  string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
		public string? PaymentIntentId { get; set; }
		public string? ClientSecret { get; set; }
        public int? deliveryMethodId { get; set; }
    }

	
}
