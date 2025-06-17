using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Repositories
{
	public interface IBasketReposiotry
	{
		// get
		Task<CustomerBasket?> GetBasketAsync(string id);

		//create and update
		Task<CustomerBasket?> UpdateBasket(CustomerBasket basket);
		// delete
		Task<bool> DeleteBasketAsync(string id);
	}
}
