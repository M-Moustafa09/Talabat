using Store.Core;
using Store.Core.Entities;
using Store.Core.Order_Aggragate;
using Store.Core.Repositories;
using Store.Core.Services;
using Store.Core.Specifications.OrderSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stor.Servics
{
	public class OrderServices : IOrderService
	{
		private readonly IBasketReposiotry _basketReposiotry;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IpaymentIntent _ipaymentIntent;

		public OrderServices(IBasketReposiotry basketReposiotry,
			IUnitOfWork unitOfWork,
			IpaymentIntent ipaymentIntent)
        {
			_basketReposiotry = basketReposiotry;
			_unitOfWork = unitOfWork;
			_ipaymentIntent = ipaymentIntent;
		}
        public async Task<Order?> CreatOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address shippingAddress)
		{
			//1.Get Basket From Basket Repo
			var Basket = await _basketReposiotry.GetBasketAsync(BasketId);
			//2. Get Selected Item From Basket From Product Repo
			var OrderItem= new List<OrderItem>();
			if(Basket?.Items.Count>0)

			{
				foreach(var item in Basket.Items)
				{
					var producr = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
					var productItemOrdered = new ProductItemOrdered(producr.Id, producr.Name, producr.PictureUrl);
					var orderItem = new OrderItem(productItemOrdered, item.Quantity, producr.Price);
					OrderItem.Add(orderItem);
				}

			}
			//3.calculate Sub Total
			var SubTotal= OrderItem.Sum(O=>O.Price * O.Quantity);
			//4.Get Delivery Method
			var deliveryMethod= await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);
			//5. Create Order
			var spec = new OrderWithpaymentintetspec(Basket.PaymentIntentId);
			var ExOrder = await _unitOfWork.Repository<Order>().GetByEntityAsyncWithSpace(spec);
			if (ExOrder is not null)
			{
				_unitOfWork.Repository<Order>().Delete(ExOrder);
				await _ipaymentIntent.CreateOrUpdatePaymentIntent(BasketId);
			}
			var order= new Order(BuyerEmail, shippingAddress, deliveryMethod,  OrderItem, SubTotal, Basket.PaymentIntentId);	
			//6. Add Order Localy

			await _unitOfWork.Repository<Order>().AddASync(order);
			//7.Add Data At DB
			var result = await _unitOfWork.ComplateAsync();
			if (result <= 0) return null;
			return order;

		}

		public async Task<Order> GetOrderByIDForSpecificUserAsync(string Email, int orderId)
		{
			var spec= new OrderSpecs(Email, orderId);
			var order= await _unitOfWork.Repository<Order>().GetByEntityAsyncWithSpace(spec);
				return order;
		}

		public async Task<IReadOnlyList<Order>> GetOrderForSpecificUserAsync(string Email)
		{
			var spec = new OrderSpecs(Email);
			var orders = await _unitOfWork.Repository<Order>().GetAllAsyncWthSpace(spec);
			return orders;
		}
	}
}
