using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Derived;

public class PhotosController : ControllerAsync<Photo>
{
    public PhotosController(IAsyncRepository<Photo> repo) : base(repo)
    {
    }

    [HttpGet("/property/{propertyId}")]
    public async Task<ActionResult<IEnumerable<Photo>>> Get(int propertyId, CancellationToken token)
    {
        return Ok(await _repository.ListAsync(p => p.PropertyId == propertyId, (0, 10), token));
    }
}
