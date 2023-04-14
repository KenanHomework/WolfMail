using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WolfMail.Extensions;

public static class JsonSerializerForControllerBase
{
    public static ContentResult SerializeIntoContent(this ControllerBase controller, object value)
    {
        var options = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve, };
        var jsonString = JsonSerializer.Serialize(value, options);
        return controller.Content(jsonString, "application/json");
    }
}
