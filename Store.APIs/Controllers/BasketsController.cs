using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.DTOs;
using Store.APIs.Errors;
using Store.Core.Entities;
using Store.Core.Repositories;

namespace Store.APIs.Controllers
{
	
	public class BasketsController : BaseController
	{
		private readonly IBasketReposiotry _basketRepos;
		private readonly IMapper _mapper;

		public BasketsController(IBasketReposiotry basketRepos, IMapper mapper)
        {
			_basketRepos = basketRepos;
			_mapper = mapper;
		}
		//get or reCreate
		[HttpGet("{BasketId}")]
		public async Task <ActionResult<CustomerBasket>> GetCustmerBasket(string BasketId)
		{
			var Basket = await _basketRepos.GetBasketAsync(BasketId);
			return Basket is null ? new CustomerBasket(BasketId) :Ok(Basket);
		}

		//Update or Create
		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
		{
			var mappedBasket=_mapper.Map<CustomerBasketDto,CustomerBasket>(basket);
			var CreatedorUpdatedBasket=await _basketRepos.UpdateBasket(mappedBasket);
			if(CreatedorUpdatedBasket is null)
			{
				return BadRequest(new ApiResponse(400));

			}
			return Ok(CreatedorUpdatedBasket);
		
		}

		//Delete
		[HttpDelete] 
		public async Task<ActionResult<bool>> DeleteCustomer(string BasketId) 
		{

			return await _basketRepos.DeleteBasketAsync(BasketId);   
		}

    }
}
