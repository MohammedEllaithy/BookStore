using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Entities
{
    class Book :EntityBase
    {
        
        public string Name { get; set; }
       
        public Category Category { get; set; }
        
        public decimal Price { get; set; }
        
    }
}
