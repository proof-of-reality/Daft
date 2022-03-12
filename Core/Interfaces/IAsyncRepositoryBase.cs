using System.Linq.Expressions;
using Core.Models.Requests;

namespace Core.Interfaces
{
    public interface IAsyncRepositoryBase<T> where T : class
    {

        /// <summary>
        /// Add an element to the underlying database
        /// </summary>
        /// <param name="entity"></param>
        public Task<T> AddAsync(T entity, CancellationToken token = default);

        /// <summary>
        /// Removes the entity from underlying database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(T entity, CancellationToken token = default);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<bool> UpdateAsync(T entity, CancellationToken token = default);

        /// <summary>
        /// Get all elements specified by query expression
        /// </summary>
        /// <param name="expression">Query</param>
        /// <returns></returns>
        public Task<List<T>> ListAsync(Expression<Func<T, bool>> expression, Pagination pagination, CancellationToken token = default, params string[] includes);

        public Task<List<T>> ListAsync(Pagination pagination, CancellationToken token = default, params string[] includes);
    }
}
