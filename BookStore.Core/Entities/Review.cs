using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Entities
{
    public class Review : EntityBase
    {
        public string Description { get; set; }

        public int Rating { get; set; }


        public Guid BookId { get; set; }
        public Book Book { get; set; }

        public Guid TenantId { get; set; }
        public  Tenant Tenant { get; set; }
    }
}
