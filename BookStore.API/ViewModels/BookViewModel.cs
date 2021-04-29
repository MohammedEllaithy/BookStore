using BookStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.ViewModels
{
    public class BookViewModel
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public Author Author { get; set; }
        public Tenant Tenant { get; set; }
    
        public ICollection<Review> Reviews { set; get; }

        // public List<Book> Books { get; set; }
    }
}
