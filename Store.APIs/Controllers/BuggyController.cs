using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stor.Reposiotry.Data;
using Store.APIs.Errors;

namespace Store.APIs.Controllers
{
	
	public class BuggyController : BaseController
	{
		private readonly StoreContext _storeContext;

		public BuggyController(StoreContext storeContext )
        {
			_storeContext = storeContext;
		}
		[HttpGet("Notfound")]
		public ActionResult GetNotFoundRequest()
		{
			var product = _storeContext.produts.Find(100);
			if (product == null)
			{
				return NotFound(new ApiResponse(404));
			}
			return Ok(product);
		}
		[HttpGet("ServerError")]
		public ActionResult GetServerErrorRequest()
		{
			var product = _storeContext.produts.Find(100);
			var ProductToReturn= product.ToString();
			return Ok(ProductToReturn);
		}
		[HttpGet("BadRequest")]
		public ActionResult GetBadRequestError()
		{
			return BadRequest();

		}
		[HttpGet("BadRequest/{id}")]
		public ActionResult GetBadRequestError(int id )
		{
			return Ok();
		}
	}
}
