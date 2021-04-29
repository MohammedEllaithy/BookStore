using BookStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Repository
{
    public interface IUnitOfWork<T> where T : class
    {
        IGenericRepository<T> Entity { get; }

        Task save();
    }
}
