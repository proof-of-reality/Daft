using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces;

public interface IAsyncRest<T> : IAsyncRestBase<T> where T : Identifiable
{
    [HttpGet("{id}")]
    Task<ActionResult<T>> GetAsync(int id, CancellationToken token = default);

    [HttpDelete("{id}")]
    Task<ActionResult<bool>> DeleteAsync(int id, CancellationToken token = default);
}
