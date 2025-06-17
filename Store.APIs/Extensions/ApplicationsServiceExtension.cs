using Microsoft.AspNetCore.Mvc;
using Stor.Reposiotry;
using Stor.Servics;
using Store.APIs.Errors;
using Store.APIs.Helper;
using Store.Core;
using Store.Core.Entities;
using Store.Core.Repositories;
using Store.Core.Services;

namespace Store.APIs.Extensions
{
	public static class ApplicationsServiceExtension
	{
		public static IServiceCollection AddApplicationService(this IServiceCollection Services )

		{
			Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfwork));
			Services.AddScoped(typeof(IOrderService), typeof(OrderServices));
			Services.AddScoped(typeof(IpaymentIntent), typeof(PaymenService));
			Services.AddScoped(typeof(IBasketReposiotry), typeof(BasketRepository));
			Services.AddScoped(typeof(IGenericRepository<>), typeof(GenaricRepository<>));// allow dependacy injection
			Services.AddAutoMapper(typeof(Mapping_Profles));
			Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var Errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
					.SelectMany(P => P.Value.Errors)
					.Select(E => E.ErrorMessage)
					.ToArray();
					var validationErrorResopnse = new ApiValidationErrorResponse()
					{
						messages = Errors
					};
					return new BadRequestObjectResult(validationErrorResopnse);
				};

			});
			return Services;


		}
	}
}
