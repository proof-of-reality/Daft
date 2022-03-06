using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Common;

public static class JsonProfile
{
    public static void Configure(this JsonSerializerOptions options)
    {
        options.IncludeFields = true;
        options.ReferenceHandler = ReferenceHandler.Preserve;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    }
}
