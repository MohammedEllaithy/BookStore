using BookStore.Core.Entities;
using BookStore.Core.Repository;
using BookStore.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly BookStoreContext _context;
        private readonly Tenant _tenant;
        private IGenericRepository<T> _entity;
        public UnitOfWork(BookStoreContext context, Tenant tenant)
        {
            _tenant = tenant;
            _context = context;
        }
        //public IGenericRepository<T> Entity
        //{
        //    get
        //    {
        //        return _entity ?? (_entity = new GenericRepository<T>(_context));
        //    }
        //}

        IGenericRepository<T> IUnitOfWork<T>.Entity
        {
            get
            {
                return _entity ?? (_entity = new GenericRepository<T>(_context, _tenant));
            }
        }

        public async Task  save()
        {
           await  _context.SaveChangesAsync();
        }
    }
}
