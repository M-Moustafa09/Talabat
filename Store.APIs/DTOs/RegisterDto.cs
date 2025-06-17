using System.ComponentModel.DataAnnotations;

namespace Store.APIs.DTOs
{
	public class RegisterDto
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

        [Required]
        public string Displayname { get; set; }
		[Required]
		[Phone]
		public string PhoneNumber { get; set; }
		[Required]
		[RegularExpression("(?=^.{6,10}$)(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&amp;*()_+]).*$",
			ErrorMessage="not valid")]
		public string Password { get; set; }
    }
}
