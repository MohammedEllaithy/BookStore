﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Entities
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        
        public ICollection<Book> Books { get; set; }
        public Guid TenantId { get; set; }
        public  Tenant Tenant { get; set; }
    }
}
