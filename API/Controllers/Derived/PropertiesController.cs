using Core.Interfaces;
using Core.Models;
using Core.Models.Requests;
using Hangfire;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Derived
{
    public class PropertiesController : ControllerAsync<Property>
    {
        public PropertiesController(IAsyncRepository<Property> repo) : base(repo)
        {
        }

        public override Task<ActionResult<Property>> Post(Property entity, CancellationToken token)
        {
            entity.Owner = null;
            return base.Post(entity, token);
        }

        [HttpGet("", Name = "Main")]
        public async Task<ActionResult<IEnumerable<Property>>> Get([FromQuery] Search pag, CancellationToken token)
        {
            return Ok(await _repository.ListAsync(p => 
                            (pag.Value == null || p.Price <= pag.Value) &&
                            (pag.Purpose == null || pag.Purpose == p.OfferPurpose) &&
                            (pag.Text == null || pag.Text.Contains(p.Address) || p.Address.Contains(pag.Text)), pag, token));
        }

        [Obsolete, NonAction]
        public override async Task<ActionResult<IEnumerable<Property>>> Get(Pagination pag, CancellationToken token)
        {
            return await base.Get(pag, token);
        }

        public override async Task<ActionResult<Property>> GetAsync(int id, CancellationToken token = default)
        {
            return Ok(await ((IAsyncRepository<Property>)_repository).GetAsync(id, token, "Photos", "Facilities", "Owner"));
        }

        [HttpPost("Email")]
        public async Task<ActionResult<string>> SendEmail([FromBody] EmailRequest request, CancellationToken token = default)
        {
            var jobId = BackgroundJob.Enqueue(() => EmailSender.Send(request));
            return Ok(jobId);
        }
    }
}
