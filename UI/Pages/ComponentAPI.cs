using Microsoft.AspNetCore.Components;

namespace UI.Pages;

public class ComponentAPI : ComponentBase
{
    [Inject] IHttpClientFactory _httpFactory { get; set; }

    protected HttpClient Endpoint(string route)
    {
        var client = _httpFactory.CreateClient("api");
        client.BaseAddress = new Uri(client.BaseAddress + route);
        return client;
    }
}
