using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.DTOs;
using Store.APIs.Errors;
using Store.Core.Entities;
using Store.Core.Services;
using System.Reflection;

namespace Store.APIs.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class PaymentController : BaseController
	{
		private readonly IpaymentIntent _ipaymentIntent;
		private readonly IMapper _mapper;

		public PaymentController(IpaymentIntent ipaymentIntent,IMapper mapper)
        {
			
			_ipaymentIntent = ipaymentIntent;
			_mapper = mapper;
		}
		[ProducesResponseType(typeof(CustomerBasketDto),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
        [HttpPost]
		public async Task< ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentintent(string BasketId)
		{
		var customerBasket=	await _ipaymentIntent.CreateOrUpdatePaymentIntent(BasketId);
			if (customerBasket is null) { return BadRequest(new ApiResponse(400, "there is a problem with your basket")); }
			var MappedBasket = _mapper.Map<CustomerBasket, CustomerBasketDto>(customerBasket);
			return Ok(MappedBasket);
		}
	}
}
