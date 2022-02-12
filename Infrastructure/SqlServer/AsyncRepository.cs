using Core.Interfaces;
using Core.Models;

namespace Infrastructure.SqlServer;

public class AsyncRepository<T> : AsyncRepositoryBase<T>, IAsyncRepository<T> where T : Identifiable
{
    public AsyncRepository(SqlServerContext db) : base(db)
    {
    }

    public Task<bool> DeleteAsync(int id, CancellationToken token = default)
    {
        var t = Table.First(t => t.Id == id);
        Table.Remove(t);
        return SaveChangesAsync(token);
    }

    public Task<T> GetAsync(int id, CancellationToken token = default)
    {
        return Task.FromResult(Table.Find((long)id)!);
    }
}
