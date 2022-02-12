using Core.Interfaces;
using Core.Models;

namespace API.Controllers.Derived
{
    public class ClientsController : ControllerAsync<Client>
    {
        public ClientsController(IAsyncRepository<Client> repo) : base(repo)
        {
        }
    }
}
