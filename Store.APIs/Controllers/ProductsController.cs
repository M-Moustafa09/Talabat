using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stor.Reposiotry;
using Store.APIs.DTOs;
using Store.APIs.Errors;
using Store.APIs.Helper;
using Store.Core;
using Store.Core.Entities;
using Store.Core.Repositories;
using Store.Core.Specifications;

namespace Store.APIs.Controllers
{

	public class ProductsController : BaseController
	{
		
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		

		public ProductsController( IMapper mapper ,IUnitOfWork unitOfWork  )//ask CLR to Inject object from Class Implement Interface IGeneraicReop
		{
			
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			
		}
		//Get All Products 
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet]
	
		public async Task<ActionResult<IReadOnlyList<Pagination<ProductToReturnDtos>>>> GetProducts([FromQuery]ProductSpecPram productSpecPram)
		{
			var spac = new producrtprandAndProductTypeSpec(productSpecPram);
			var result = await _unitOfWork.Repository<Product>().GetAllAsyncWthSpace(spac);
			var mappedproducts = _mapper.Map<IReadOnlyList<  Product>, IReadOnlyList< ProductToReturnDtos> >(result);
			var countspec = new CountSpacifications(productSpecPram);
			var count = await _unitOfWork.Repository<Product>().GetCountOfProductsBySpec(countspec);
			//var PaginationResult = new Pagination<ProductToReturnDtos>()
			//{
			//	PageIndex = productSpecPram.PageIndex,
			//	PageSize = productSpecPram.PageSize,
			//	Data = mappedproducts,
			//	Count = count
			//};

				
			return Ok(new Pagination<ProductToReturnDtos>(productSpecPram.PageIndex, productSpecPram.PageSize, mappedproducts, count));
		}
		//GEt Product By ID
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(Product), 200)]
		[ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ProductToReturnDtos>> GetProduct(int id)
		{
			var spec= new producrtprandAndProductTypeSpec(id);
			var result= await _unitOfWork.Repository<Product>().GetByEntityAsyncWithSpace(spec);
			if (result == null) { return NotFound(new ApiResponse(404)); }
			var MappedProduct= _mapper.Map<Product, ProductToReturnDtos>(result);
			return Ok(MappedProduct);
		}

		[HttpGet("Types")]
		public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
		{
			var Types = await _unitOfWork.Repository<ProductType>().GetAllAsync();
			return Ok(Types);
		}

		[HttpGet("Brands")]

		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrand()
		{
			var Brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
			return Ok(Brands);
		}




	}

	
}
