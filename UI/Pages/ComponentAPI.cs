using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MyUser = Core.Models.User;

namespace UI.Pages;

public class ComponentAPI : LayoutComponentBase
{
    [Inject] ProtectedLocalStorage _browserStorage { get; set; }
    [Inject] IHttpClientFactory _httpFactory { get; set; }

    public async Task<MyUser> GetUser()
    {
        var res = await _browserStorage.GetAsync<MyUser>(nameof(Core.Models.User).ToLower());
        return res.Value!;
    }

    protected async Task Set(MyUser value)
    {
        value.Password = "";
        await _browserStorage.SetAsync(nameof(Core.Models.User).ToLower(), value).ConfigureAwait(false);
    }

    protected HttpClient Endpoint(string route)
    {
        var client = _httpFactory.CreateClient("api");
        client.BaseAddress = new Uri(client.BaseAddress + route);
        return client;
    }
}
