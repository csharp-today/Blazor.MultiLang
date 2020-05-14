namespace CSharpToday.Blazor.MultiLang.Resources.Value
{
    internal class JsonValueProviderFactory : IJsonValueProviderFactory
    {
        public IValueProvider GetJsonValueProvider(string json) => new JsonValueProvider(json);
    }
}
