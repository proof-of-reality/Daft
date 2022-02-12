using Core.Interfaces;
using Core.Models;

namespace API.Controllers.Derived
{
    public class FacilitiesController : ControllerAsync<Facility>
    {
        public FacilitiesController(IAsyncRepository<Facility> repo) : base(repo)
        {
        }
    }
}
