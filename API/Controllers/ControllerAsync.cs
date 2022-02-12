using API.Interfaces;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ControllerAsync<T> : BaseControllerAsync<T>, IAsyncRest<T> where T : Identifiable
{
    protected IAsyncRepository<T> Repository => (IAsyncRepository<T>)_repository;

    public ControllerAsync(IAsyncRepository<T> repo) : base(repo)
    {
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<T>> GetAsync(int id, CancellationToken token = default) =>
        Ok(await Repository.GetAsync(id, token));

    [HttpDelete("{id}")]
    public virtual async Task<ActionResult<bool>> DeleteAsync(int id, CancellationToken token = default) =>
        Ok(await Repository.DeleteAsync(id, token));
}
