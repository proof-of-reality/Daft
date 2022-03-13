using Core.Interfaces;
using Core.Models;
using Core.Models.Requests;

namespace API.Controllers.Derived;

public class FacilitiesController : ControllerAsync<Facility, Pagination>
{
    public FacilitiesController(IAsyncRepository<Facility> repo) : base(repo)
    {
    }
}
