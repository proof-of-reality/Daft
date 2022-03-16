using Core.Models;
using Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces;

public interface IAsyncRest<T>  where T : Identifiable
{
    [HttpGet("{id}")]
    Task<ActionResult<T>> GetAsync(long id, CancellationToken token = default);

    [HttpDelete("{id}")]
    Task<ActionResult<bool>> DeleteAsync(long id, CancellationToken token = default);
}
