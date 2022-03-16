using API.Interfaces;
using Core.Interfaces;
using Core.Models;
using Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ControllerAsync<T, Search> : BaseControllerAsync<T, Search>, IAsyncRest<T> where T : Identifiable where Search : Pagination
{
    protected IAsyncRepository<T> Repository => (IAsyncRepository<T>)_repository;

    public ControllerAsync(IAsyncRepository<T> repo) : base(repo)
    {
    }

    [HttpGet("{id}")]
    public virtual Task<ActionResult<T>> GetAsync(long id, CancellationToken token = default) =>
        Invoke<long, T>(async (@id, ct) => await Repository.GetAsync(@id, ct))(id, token);

    [HttpDelete("{id}")]
    public virtual Task<ActionResult<bool>> DeleteAsync(long id, CancellationToken token = default) =>
        Invoke<long, bool>(Repository.DeleteAsync)(id, token);
}
