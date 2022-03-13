using Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces;

public interface IAsyncRestBase<T, in Search> where T : class where Search : Pagination
{
    [HttpPost]
    public Task<ActionResult<T>> Post(T entity, CancellationToken token);

    [HttpGet]
    public Task<ActionResult<IEnumerable<T>>> Get(Search pag, CancellationToken token);

    [HttpPatch]
    public Task<ActionResult<bool>> Patch(T entity, CancellationToken token);

    [HttpDelete]
    public Task<ActionResult<bool>> Delete(T entity, CancellationToken token);
}
