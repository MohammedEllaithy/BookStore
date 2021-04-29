using BookStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.ViewModels
{
    public class TenantViewModel
    {
        public List<Book> Books { get; set; }
    }
}
