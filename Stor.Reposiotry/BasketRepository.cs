using StackExchange.Redis;
using Store.Core.Entities;
using Store.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Stor.Reposiotry
{
	public class BasketRepository : IBasketReposiotry
	{
		private readonly IDatabase _Database;

		public BasketRepository(IConnectionMultiplexer redis) 
		{
			_Database = redis.GetDatabase();
		}
		public async Task<bool> DeleteBasketAsync(string id)
		{
			return await _Database.KeyDeleteAsync(id);
		}

		public async Task<CustomerBasket?> GetBasketAsync(string id)
		{
			var Basket = await _Database.StringGetAsync(id);
			return Basket.IsNull?null :JsonSerializer.Deserialize<CustomerBasket>(Basket);
		}

		public async Task<CustomerBasket?> UpdateBasket(CustomerBasket basket)
		{
			var jsonBasket = JsonSerializer.Serialize(basket);
			var CreatedorUpdated = await _Database.StringSetAsync(basket.Id, jsonBasket, TimeSpan.FromDays(1));

			if(!CreatedorUpdated) return null;
			return await GetBasketAsync(basket.Id);
		}
	}
}
