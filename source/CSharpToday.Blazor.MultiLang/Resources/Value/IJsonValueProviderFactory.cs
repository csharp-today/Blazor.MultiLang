namespace CSharpToday.Blazor.MultiLang.Resources.Value
{
    internal interface IJsonValueProviderFactory
    {
        IValueProvider GetJsonValueProvider(string json);
    }
}
