using Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces;

public interface IAsyncRestBase<T> where T : class
{
    [HttpPost]
    public Task<ActionResult<T>> Post(T entity, CancellationToken token);

    [HttpGet]
    public Task<ActionResult<IEnumerable<T>>> Get(Pagination pag, CancellationToken token);

    [HttpPatch]
    public Task<ActionResult<bool>> Patch(T entity, CancellationToken token);

    [HttpDelete]
    public Task<ActionResult<bool>> Delete(T entity, CancellationToken token);
}
