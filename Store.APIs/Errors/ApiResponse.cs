namespace Store.APIs.Errors
{
	public class ApiResponse
	{
        public int? StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int code, string? message=null) 
        {
			StatusCode=code;
            Message=message?? GetDefualtMessage(code);

		}
        private string? GetDefualtMessage(int? code)
        {
            return code switch
            {
                400 => "Bad Request",
                401 => "you are not Authorized",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => null
                
            };
        }





    }
}
