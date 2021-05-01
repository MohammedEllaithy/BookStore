using BookStore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <returns>List of entities</returns>
        IEnumerable<T> GetAll(
       Expression<Func<T, bool>> filter = null,
       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
       string includeProperties = "");


        /// <summary>
        /// Get Entity by it's id asynchrons
        /// and throw exception if there is more than one item  is matching the condition
        /// </summary>
        /// <typeparam name="T">string</typeparam>
        /// <returns>Single entity</returns>
        Task<T> GetByIdAsync(Guid id);
        /// <summary>
        /// Add new entity async
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Added entity with it's id</returns>
        void Add(T entity);

        /// <summary>
        /// Update existing entity async
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Updated entity with it's id</returns>
        void Update(T entity);
        /// <summary>
        /// Delete entity async
        /// </summary>
        /// <typeparam name="T">Entity Type</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Number of state entries written to the database.</returns>
        void Delete(Guid id);
    }
}
