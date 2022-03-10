using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Derived;

public class UsersController : ControllerAsync<Client>
{
    public UsersController(IAsyncRepository<Client> repo) : base(repo)
    {
    }

    async Task<Client> GetAsync(User user, CancellationToken token) => 
        (await _repository.ListAsync(u => user.Email.Equals(u.Email), (0, 1), token)).FirstOrDefault()!;

    public override async Task<ActionResult<Client>> Post(Client user, CancellationToken token)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.ValidationState);
        var _user = await GetAsync(user, token);

        if(_user != null)
        {
            ModelState.AddModelError("all", "email already exists");
            return BadRequest(ModelState.IsValid);
        }

        EncryptPassword(user);
        return await base.Post(user, token);
    }

    private static void EncryptPassword(User user)
    {
        var encrypted = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Password = encrypted;
    }


    [HttpPost("Login")]
    public async Task<ActionResult<Client>> Login(User user, CancellationToken token)
    {
        var _user = await GetAsync(user, token);

        if (_user is null) return BadRequest("User doesnt exists");
        var res = BCrypt.Net.BCrypt.Verify(user.Password, _user.Password);

        _user.Password = "";
        return res ? Ok(_user) : BadRequest("Wrong password");
    }
}
