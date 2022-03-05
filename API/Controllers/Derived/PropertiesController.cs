using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Derived
{
    public class PropertiesController : ControllerAsync<Property>
    {
        public PropertiesController(IAsyncRepository<Property> repo) : base(repo)
        {
        }

        public override Task<ActionResult<bool>> Post(Property entity, CancellationToken token)
        {
            return base.Post(entity, token);
        }
    }
}
