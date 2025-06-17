using Store.Core.Order_Aggragate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Services
{
	public interface IOrderService
	{
		Task<Order?> CreatOrderAsync(string BuyerEmail, String BasketId, int DeliveryMethodId, Address shippingAddress);
		Task<IReadOnlyList<Order>> GetOrderForSpecificUserAsync(string Email);
		Task<Order> GetOrderByIDForSpecificUserAsync(string Email, int orderId);

	}
}
