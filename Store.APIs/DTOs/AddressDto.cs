using System.ComponentModel.DataAnnotations;

namespace Store.APIs.DTOs
{
	public class AddressDto
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string FristName { get; set; }
		[Required]
		public string LAstName { get; set; }
		[Required]

		public string City { get; set; }
		[Required]
		public string Street { get; set; }
		[Required]
		public string Country { get; set; }
	}
}
