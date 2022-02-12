using API.Interfaces;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseControllerAsync<T> : ControllerBase, IAsyncRestBase<T> where T : class
{
    protected readonly IAsyncRepositoryBase<T> _repository;

    public BaseControllerAsync(IAsyncRepositoryBase<T> repository)
    {
        _repository = repository;
    }

    [HttpDelete]
    public virtual async Task<ActionResult<bool>> Delete(T entity, CancellationToken token) =>
        Ok(await _repository.DeleteAsync(entity, token));

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<T>>> Get(int from, int quantity, CancellationToken token) =>
        Ok(await _repository.ListAsync((from, quantity), token));

    [HttpPatch]
    public virtual async Task<ActionResult<bool>> Patch(T entity, CancellationToken token) =>
        Ok(await _repository.UpdateAsync(entity, token));

    [HttpPost]
    public virtual async Task<ActionResult<bool>> Post(T entity, CancellationToken token) =>
        Ok(await _repository.AddAsync(entity, token));
}
