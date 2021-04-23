using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Entities
{
    public class Book :EntityBase
    {
        
        public string Name { get; set; }
       
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }

        public  Author Author { get; set; }
        
        public Guid AuthorId { get; set; }
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public ICollection<Review> Reviews { set; get; }

    }
}
