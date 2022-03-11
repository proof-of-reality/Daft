using Core.Interfaces;
using Core.Models;

namespace Infrastructure.SqlServer;

public class AsyncRepository<T> : AsyncRepositoryBase<T>, IAsyncRepository<T> where T : Identifiable
{
    public AsyncRepository(SqlServerContext db) : base(db)
    {
    }

    public virtual Task<bool> DeleteAsync(int id, CancellationToken token = default)
    {
        var t = Table.First(t => t.Id == id);
        Table.Remove(t);
        return SaveChangesAsync(token);
    }

    public virtual async Task<T> GetAsync(int id, CancellationToken token = default, params string[] includes)
    {
        return (await Execute(Table.Where(e => e.Id == id).Take(1), token, includes)).First();
    }
}
