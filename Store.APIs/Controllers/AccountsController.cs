using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.DTOs;
using Store.APIs.Errors;
using Store.APIs.Extensions;
using Store.Core.Entities.Identity;
using Store.Core.Services;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;

namespace Store.APIs.Controllers
{
	
	public class AccountsController : BaseController
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;
		private readonly IMapper _mapper;

		public AccountsController(UserManager<AppUser> userManager,
			                      SignInManager<AppUser> signInManager,
								  ITokenService tokenService,
								  IMapper mapper

			) 
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
			_mapper = mapper;
		}


		//Register
		[HttpPost("Register")]
		public async Task <ActionResult<UserDto>> Register(RegisterDto model)
		{
			if(CheckEmailExist(model.Email).Result.Value) {

				return BadRequest(new ApiResponse(400, "Email is Already in use"));

			}
			var Uer = new AppUser()
			{
				Displayname = model.Displayname,
				Email = model.Email,
				UserName= model.Email.Split('@')[0],
				PhoneNumber = model.PhoneNumber,
			};
		var result=	await _userManager.CreateAsync(Uer,model.Password);
			 if(!result.Succeeded)
			{
				return BadRequest(new ApiResponse(400));
			}
			var ReturendUseer = new UserDto()
			{
				DisplayName = model.Displayname,
				Email = model.Email,
				Token = await _tokenService.CreateTokenAsync(Uer, _userManager)

			};
			return Ok(ReturendUseer);

		}
		//login 
		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		 {
			var User = await _userManager.FindByEmailAsync(model.Email);
			if (User is null)
			{
               return  Unauthorized(new ApiResponse(401));
			}
			var Result= await _signInManager.CheckPasswordSignInAsync(User, model.Password,false);
			if (!Result.Succeeded) { return Unauthorized(new ApiResponse(401)); }

			return Ok(new UserDto()
			{
				DisplayName = User.Displayname,
				Email = User.Email,
				Token = await _tokenService.CreateTokenAsync(User, _userManager)

			});
		}
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet("GetCurrntUser")]
		public async Task<ActionResult<UserDto>> GetCurrntUser()
		{
			var Email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.FindByEmailAsync(Email);
			var RetrnedResult = new UserDto()
			{
				DisplayName = user.Displayname,
				Email = user.Email,
				Token = await _tokenService.CreateTokenAsync(user, _userManager)

			};
			return Ok(RetrnedResult);
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet("Address")]
		public async Task<ActionResult<AddressDto>> GetUserAddress()
		{
			var user = await _userManager.FindUserWithAdressAsync(User);
			var mappedObject = _mapper.Map<Address, AddressDto>(user.address);

			return Ok(mappedObject);
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpPut("Address")]
		public async Task<ActionResult<AddressDto>> UpdateAdress(AddressDto UpdatedAddress)
		{
			var user =await _userManager.FindUserWithAdressAsync(User);
			var mappedAddress =  _mapper.Map<AddressDto, Address>(UpdatedAddress);
			mappedAddress.Id= user.address.Id;
			user.address = mappedAddress;
			var result =await _userManager.UpdateAsync(user);
			if(!result.Succeeded) return BadRequest(new ApiResponse(400));
			return Ok(UpdatedAddress);


		}
		[HttpGet("emailExists")]
		public async Task<ActionResult<bool>> CheckEmailExist(string email)
		{
			return await _userManager.FindByEmailAsync(email) is not null;
		}


	}
}
