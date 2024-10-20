using System.Text.Json;

namespace Yosan.Gateway.Helpers;

public static class CheckRequestPath
{
    public static string Check(string json)
    {
        using JsonDocument doc = JsonDocument.Parse(json);
        JsonElement root = doc.RootElement;

        var fields = root.EnumerateObject();

        foreach (var field in fields)
        {
            if (field.Name == "Path")
            {
                return field.Value.ToString();
            }
        }
        
        return "";
    }
}