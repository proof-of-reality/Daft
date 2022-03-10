using System.Linq.Expressions;
using Core.Interfaces;
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

    public async Task<T> AddAsync(T entity, CancellationToken token = default)
    {
        Table.Add(entity);
        await SaveChangesAsync(token);
        return entity;
    }

    public Task<bool> DeleteAsync(T entity, CancellationToken token = default)
    {
        Table.Remove(entity);
        return SaveChangesAsync(token);
    }

    public void Dispose()
    {
        _db.Dispose();
        GC.SuppressFinalize(this);
    }

    public Task<List<T>> ListAsync(Expression<Func<T, bool>> expression, (int from, int qtty) pagination, CancellationToken token = default) =>
        Table.Where(expression).Skip(pagination.from).Take(pagination.qtty).ToListAsync(token);

    public Task<List<T>> ListAsync((int from, int qtty) pagination, CancellationToken token = default) =>
        Table.Skip(pagination.from).Take(pagination.qtty).ToListAsync(token);

    public Task<bool> UpdateAsync(T entity, CancellationToken token = default)
    {
        Table.Update(entity);
        return SaveChangesAsync(token);
    }

    protected async Task<bool> SaveChangesAsync(CancellationToken token = default) => (await _db.SaveChangesAsync(token)) > 0;
}
