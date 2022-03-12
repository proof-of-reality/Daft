using System.Linq.Expressions;
using Core.Interfaces;
using Core.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SqlServer;

public class AsyncRepositoryBase<T> : IAsyncRepositoryBase<T>, IDisposable where T : class
{
    protected readonly SqlServerContext _db;

    protected DbSet<T> Table => _db.Set<T>();

    public AsyncRepositoryBase(SqlServerContext db)
    {
        _db = db;
    }

    public virtual async Task<T> AddAsync(T entity, CancellationToken token = default)
    {
        Table.Add(entity);
        await SaveChangesAsync(token);
        return entity;
    }

    public virtual Task<bool> DeleteAsync(T entity, CancellationToken token = default)
    {
        Table.Remove(entity);
        return SaveChangesAsync(token);
    }

    public void Dispose()
    {
        _db.Dispose();
        GC.SuppressFinalize(this);
    }

    public virtual Task<List<T>> ListAsync(Expression<Func<T, bool>> expression, Pagination pagination, CancellationToken token = default, params string[] includes)
    {
        return Execute(Table.Where(expression).Skip(pagination.From).Take(pagination.Quantity), token, includes);
    }
    public virtual Task<List<T>> ListAsync(Pagination pagination, CancellationToken token = default, params string[] includes)
    {
        return Execute(Table.Skip(pagination.From).Take(pagination.Quantity), token, includes);
    }

    protected static async Task<List<T>> Execute(IQueryable<T> query, CancellationToken token = default, params string[] includes)
    {
        foreach (var property in includes)
        {
            query = query.Include(property);
        }
        return await query.ToListAsync(token);
    }

    public virtual Task<bool> UpdateAsync(T entity, CancellationToken token = default)
    {
        Table.Update(entity);
        return SaveChangesAsync(token);
    }

    protected async Task<bool> SaveChangesAsync(CancellationToken token = default) => (await _db.SaveChangesAsync(token)) > 0;
}
