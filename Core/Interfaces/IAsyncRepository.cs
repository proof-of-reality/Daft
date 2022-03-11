using Core.Models;

namespace Core.Interfaces;

public interface IAsyncRepository<T> : IAsyncRepositoryBase<T> where T : Identifiable
{
    /// <summary>
    /// Get T:entity by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>T: Entity</returns>
    Task<T> GetAsync(int id, CancellationToken token = default, params string[] includes);

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(int id, CancellationToken token = default);
}
