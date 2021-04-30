using BookStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.ViewModels
{
    public class ReviewViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public int Rating { get; set; }
       
        public Book Book { get; set; }
        public Tenant Tenant { get; set; }
    }
}
