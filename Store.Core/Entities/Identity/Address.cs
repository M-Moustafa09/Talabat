﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FristName { get; set; }
        public string LAstName { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string AppUserId { get; set; }
        public AppUser User  { get; set; }
    }
}
