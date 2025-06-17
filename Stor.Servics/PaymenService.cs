using Microsoft.Extensions.Configuration;
using Store.Core;
using Store.Core.Entities;
using Store.Core.Order_Aggragate;
using Store.Core.Repositories;
using Store.Core.Services;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Store.Core.Entities.Product;

namespace Stor.Servics
{
	public class PaymenService : IpaymentIntent
	{
		private readonly IConfiguration _configuration;
		private readonly IBasketReposiotry _basketReposiotry;
		private readonly IUnitOfWork _unitOfWork;

		public PaymenService(IConfiguration configuration,
							 IBasketReposiotry basketReposiotry,
							 IUnitOfWork unitOfWork
			                 
			                  )
		{
			_configuration = configuration;
			_basketReposiotry = basketReposiotry;
			_unitOfWork = unitOfWork;
		}
		public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId)
		{
			//1.Secret Key
			StripeConfiguration.ApiKey = _configuration["stripeKeys:Secretkey"];
			//2.Get Basket 
			var Basket=await _basketReposiotry.GetBasketAsync(BasketId );
			if (Basket is null) return null;
		     var Shippingprice = 0m;
			if (Basket.DeliveryMethodId.HasValue)	
			{
				var DeliveryMethod=await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(Basket.DeliveryMethodId.Value);
				Shippingprice = DeliveryMethod.Cost;
			}
			if(Basket.Items.Count > 0)
			{
				foreach( var item in Basket.Items )
				{
					var Product =await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
					if(item.Price!=Product.Price)
					{ item.Price = Product.Price; }
				}
			}
			//total = SubTotal + DM Cost
			var SubTotal = Basket.Items.Sum(I=>I.Price*I.Quantity);
			//Create PaymentIntent
			var Service = new PaymentIntentService();
			PaymentIntent paymentIntent;
			if (string.IsNullOrEmpty(Basket.PaymentIntentId))//create
			{
				var opition = new PaymentIntentCreateOptions()
				{
					Amount=(long)(SubTotal*100+ Shippingprice*100),
					Currency="usd",
					PaymentMethodTypes=new List<string>() { "card"}


				};
				paymentIntent=await Service.CreateAsync(opition);
				Basket.PaymentIntentId= paymentIntent.Id;
				Basket.ClientSecret=Basket.ClientSecret;
			}
			else   // Update
			{
				var opitions = new PaymentIntentUpdateOptions()
				{
					Amount = (long)(SubTotal * 100 + Shippingprice * 100),
				};
				 paymentIntent = await Service.UpdateAsync(Basket.PaymentIntentId,opitions);
				Basket.PaymentIntentId = paymentIntent.Id;
				Basket.ClientSecret = Basket.ClientSecret;
			}
			await _basketReposiotry.UpdateBasket(Basket);
			return Basket;
				
		}
	}

}
