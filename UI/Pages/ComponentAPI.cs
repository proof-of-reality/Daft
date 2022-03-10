using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Newtonsoft.Json;

namespace UI.Pages;

public class ComponentAPI : LayoutComponentBase
{
    [Inject] ProtectedLocalStorage _browserStorage { get; set; }
    [Inject] IHttpClientFactory _httpFactory { get; set; }

    public async Task<Core.Models.Client> GetCurrentUser()
    {
        var res = await _browserStorage.GetAsync<string>(nameof(Core.Models.Client).ToLower());

        var client = JsonConvert.DeserializeObject<Core.Models.Client>(res.Value!);
        client.Password = Guid.NewGuid().ToString();
        return client;
    }

    protected async Task Set(Core.Models.Client value)
    {
        await _browserStorage.SetAsync(nameof(Core.Models.Client).ToLower(), JsonConvert.SerializeObject(value)).ConfigureAwait(false);
    }

    protected HttpClient Endpoint(string route)
    {
        var client = _httpFactory.CreateClient("api");
        client.BaseAddress = new Uri(client.BaseAddress + route);
        return client;
    }
}
