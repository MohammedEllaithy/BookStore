using BookStore.Core.Entities;
using BookStore.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BookStoreContext _context;
        private readonly Tenant _tenant;
        private DbSet<T> table = null;
        public GenericRepository(BookStoreContext context, Tenant tenant)
        {
            _context = context;
            _tenant = tenant;
            table = _context.Set<T>();
        }

        public void Add(T entity)
        {
            table.Add(entity);
        }

        public async void  Delete(object id)
        {
            T existing = await GetByIdAsync(id);
            table.Remove(existing);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
           
            IQueryable<T> query = (IQueryable<T>)table;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }


            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

     

        public async Task<T> GetByIdAsync(object id)
        {
            return await table.FindAsync(id);
        }

        public void Update(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        
    }
}
