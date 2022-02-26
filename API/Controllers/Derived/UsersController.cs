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
        EncryptPassword(entity);
        return base.Post(entity, token);
    }

    private static void EncryptPassword(User user)
    {
        var encrypted = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Password = encrypted;
    }

    public override Task<ActionResult<bool>> Patch(User entity, CancellationToken token)
    {
        EncryptPassword(entity);
        return base.Patch(entity, token);
    }

    [HttpPost("Login")]
    public virtual async Task<ActionResult<bool>> Login(User user, CancellationToken token)
    {
        var userRepo = await _repository.ListAsync(u => u.Email.Equals(user.Email) && u.Password.Equals(u.Password), (0, 1), token);
        return userRepo is not null;
    }
}
