﻿using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Services
{
	public interface IpaymentIntent
	{
		Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId);
	}
}
