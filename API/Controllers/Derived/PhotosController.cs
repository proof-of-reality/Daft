using Core.Interfaces;
using Core.Models;

namespace API.Controllers.Derived;

public class PhotosController : ControllerAsync<Photo>
{
    public PhotosController(IAsyncRepository<Photo> repo) : base(repo)
    {
    }
}
