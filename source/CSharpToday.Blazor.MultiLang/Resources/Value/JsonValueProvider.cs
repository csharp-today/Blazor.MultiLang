using System.Linq;
using System.Text.Json;

namespace CSharpToday.Blazor.MultiLang.Resources.Value
{
    internal class JsonValueProvider : IValueProvider
    {
        private readonly JsonDocument _json;

        public JsonValueProvider(string json) => _json = json is { } ? JsonDocument.Parse(json) : null;

        public string GetValue(string key) => _json is { } ? GetValue(_json.RootElement, key) : null;

        private string GetValue(JsonElement json, string key)
        {
            if (json.TryGetProperty(key, out var property))
            {
                return property.GetString();
            }

            var keyParts = key.Split('.');
            if (keyParts.Length == 1)
            {
                return null;
            }

            for (int count = 1; count < keyParts.Length; count++)
            {
                var masterKey = string.Join(".", keyParts.Take(count));
                if (json.TryGetProperty(masterKey, out property))
                {
                    var secondaryKey = string.Join(".", keyParts.Skip(count));
                    var secondaryValue = GetValue(property, secondaryKey);
                    if (secondaryValue is { })
                    {
                        return secondaryValue;
                    }
                }
            }

            return null;
        }
    }
}
