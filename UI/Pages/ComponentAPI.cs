using Microsoft.AspNetCore.Components;

namespace UI.Pages;

public class ComponentAPI : ComponentBase
{
    [Inject] IHttpClientFactory _httpFactory { get; set; }

    protected HttpClient GetClient() => _httpFactory.CreateClient("api");
}
