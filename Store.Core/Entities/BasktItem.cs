﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities
{
	public class BasktItem
	{
        public int  Id { get; set; }
        public string productname { get; set; }

        public string PictureUrl { get; set; }

        public string Brand { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }


    }
}
