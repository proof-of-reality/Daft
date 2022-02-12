using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces;

public interface IAsyncRestBase<T> where T : class
{
    [HttpPost]
    public Task<ActionResult<bool>> Post(T entity, CancellationToken token);

    [HttpGet]
    public Task<ActionResult<IEnumerable<T>>> Get(int from, int quantity, CancellationToken token);

    [HttpPatch]
    public Task<ActionResult<bool>> Patch(T entity, CancellationToken token);

    [HttpDelete]
    public Task<ActionResult<bool>> Delete(T entity, CancellationToken token);
}
