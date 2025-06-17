using System.ComponentModel.DataAnnotations;

namespace Store.APIs.DTOs
{
	public class BasketItemDto
	{

		[Required]
		public int Id { get; set; }
		[Required]
		public string productname { get; set; }
		[Required]
		public string PictureUrl { get; set; }
		[Required]
		
		public string Brand { get; set; }
		[Required]
		public string Type { get; set; }
		[Required]
		[Range(1, double.MaxValue, ErrorMessage = "price can not be zero")]
		public decimal Price { get; set; }
		[Required]
		[Range(1,int.MaxValue,ErrorMessage ="Quantity must beone item at least")]
		public int Quantity { get; set; }
	}
}