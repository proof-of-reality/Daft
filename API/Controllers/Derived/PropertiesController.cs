using Core.Interfaces;
using Core.Models;

namespace API.Controllers.Derived
{
    public class PropertiesController : ControllerAsync<Property>
    {
        public PropertiesController(IAsyncRepository<Property> repo) : base(repo)
        {
        }
    }
}
