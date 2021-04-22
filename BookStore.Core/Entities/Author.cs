using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Entities
{
    class Author : EntityBase
    {
        
        
        public string Name { get; set; }
       
        
        public string Nationality { get; set; }
        
       
        public ICollection<Book> Books { get; set; }
    }
}
