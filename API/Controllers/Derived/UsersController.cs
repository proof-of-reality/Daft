using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Derived;

public class UsersController : ControllerAsync<User>
{
    public UsersController(IAsyncRepository<User> repo) : base(repo)
    {
    }

    public override Task<ActionResult<bool>> Post(User entity, CancellationToken token)
    {
        //TODO: encriptar senha
        entity.Password = "fjnefgehnjofieknml";
        return base.Post(entity, token);
    }
}
