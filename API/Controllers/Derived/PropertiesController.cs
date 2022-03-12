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

		public override async Task<ActionResult<Property>> GetAsync(int id, CancellationToken token = default)
		{
            return Ok(await ((IAsyncRepository<Property>)_repository).GetAsync(id, token, "Photos", "Facilities", "Owner"));
        }
    }
}
