namespace Store.APIs.Errors
{
	public class ApiValidationErrorResponse:ApiResponse
	{
        public IEnumerable<string> messages  { get; set; }
        public ApiValidationErrorResponse():base(400)
        {
            messages = new List<string>();
        }
    }
}
