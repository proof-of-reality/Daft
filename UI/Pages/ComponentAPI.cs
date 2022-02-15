using Microsoft.AspNetCore.Components;

namespace UI.Pages;

public abstract class ComponentAPI : ComponentBase
{
    [Inject]
    private IHttpClientFactory _ttpFactory { get; set; }

    public string Host = "https://localhost:7255/api/";

    protected HttpClient GetClient() => _ttpFactory.CreateClient("SSLUntrusted");

    public static string operator +(ComponentAPI c, string route) => c.Host + route;
}
