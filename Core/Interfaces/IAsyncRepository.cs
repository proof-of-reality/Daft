using Core.Models;

namespace Core.Interfaces;

public interface IAsyncRepository<T> : IAsyncRepositoryBase<T> where T : Identifiable
{
    /// <summary>
    /// Get T:entity by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>T: Entity</returns>
    Task<T> GetAsync(long id, CancellationToken token = default, params string[] includes);

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(long id, CancellationToken token = default);
}
