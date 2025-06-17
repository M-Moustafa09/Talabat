using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.DTOs;
using Store.APIs.Errors;
using Store.Core;
using Store.Core.Order_Aggragate;
using Store.Core.Services;
using System.Security.Claims;

namespace Store.APIs.Controllers
{

	public class OrdersController : BaseController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public OrdersController(IOrderService orderService, IMapper mapper,IUnitOfWork unitOfWork)
		{
			_orderService = orderService;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}
		[ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

		[HttpPost]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult<Order>> CreatOrder(OrderDto orderDto)
		{
			var Email = User.FindFirstValue(ClaimTypes.Email);
			var MAppedAddress = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
			var order = await _orderService.CreatOrderAsync(Email, orderDto.BasketId, orderDto.DeliveryMethodId, MAppedAddress);
			if (order is null) return BadRequest(new ApiResponse(400, "Error With your order"));
			return Ok(order);

		}
		[ProducesResponseType(typeof(IReadOnlyList<OrderToRetuernDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<ActionResult<IReadOnlyList<OrderToRetuernDto>>> GetOrderForUser()
		{
			var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
			var orders = await _orderService.GetOrderForSpecificUserAsync(BuyerEmail);
			if (orders is null) return NotFound(new ApiResponse(404, "there is no order for this User"));
			var mappedorderd = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToRetuernDto>>(orders);
			return Ok(mappedorderd);

		}
		[ProducesResponseType(typeof(OrderToRetuernDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderToRetuernDto>> GetOrderByIdForUser(int id)
		{
			var buyerEmail= User.FindFirstValue(ClaimTypes.Email);
			var order=await _orderService.GetOrderByIDForSpecificUserAsync(buyerEmail, id);
			if (order is null) return NotFound(new ApiResponse(404, $"There is no Order For this {id} User"));
			var mappedorder = _mapper.Map<Order, OrderToRetuernDto>(order);
			return Ok(mappedorder	);
		}

		[HttpGet("DeliveryMethods")]
		public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDilveryMethods()
		{
			var deliveyMethods= await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
			return Ok(deliveyMethods);

		}

	}
}
