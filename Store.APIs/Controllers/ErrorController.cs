using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.Errors;

namespace Store.APIs.Controllers
{
	[Route("error/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]	
	public class ErrorController : ControllerBase
	{
		public ActionResult Error(int code)
		{
			return NotFound(new ApiResponse(code));
		}
	}
}
