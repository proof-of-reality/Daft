using API.Interfaces;
using Core.Interfaces;
using Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController, Route("api/[controller]")]
public class BaseControllerAsync<T, Search> : ControllerBase, IAsyncRestBase<T, Search> where T : class where Search : Pagination
{
    protected readonly IAsyncRepositoryBase<T> _repository;

    /// <summary>
    /// Class constructor injected by .NET 6 framework
    /// </summary>
    /// <param name="repository"></param>
    public BaseControllerAsync(IAsyncRepositoryBase<T> repository) => _repository = repository;

    [HttpDelete]
    public virtual Task<ActionResult<bool>> Delete(T entity, CancellationToken token) =>
        Invoke<T, bool>(_repository.DeleteAsync)(entity, token);

    [HttpGet]
    public virtual Task<ActionResult<IEnumerable<T>>> Get([FromQuery] Search pag, CancellationToken token) =>
        Invoke<Search, IEnumerable<T>>(async (t, ct) => await _repository.ListAsync(t, ct))(pag, token);

    [HttpPatch]
    public virtual Task<ActionResult<bool>> Patch(T entity, CancellationToken token) =>
        Invoke<T, bool>(_repository.UpdateAsync)(entity, token);

    [HttpPost]
    public virtual Task<ActionResult<T>> Post(T entity, CancellationToken token) => Invoke<T, T>(_repository.AddAsync)(entity, token);

    protected Func<TIn, CancellationToken, Task<ActionResult<TOut>>> Invoke<TIn, TOut>(Func<TIn, CancellationToken, Task<TOut>> func) =>
        async (entity, tk) => ModelState.IsValid ? Ok(await func(entity, tk)) : BadRequest(ModelState);
}
