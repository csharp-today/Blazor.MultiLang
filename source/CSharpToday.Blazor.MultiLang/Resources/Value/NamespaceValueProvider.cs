namespace CSharpToday.Blazor.MultiLang.Resources.Value
{
    internal class NamespaceValueProvider : IValueProvider
    {
        private readonly string _namespaceAndDot;
        private readonly IValueProvider _valueProvider;

        public NamespaceValueProvider(string namespaceName, IValueProvider valueProvider) =>
            (_namespaceAndDot, _valueProvider) = ($"{namespaceName}.", valueProvider);

        public string GetValue(string key)
        {
            if (_namespaceAndDot == ".")
            {
                return _valueProvider.GetValue(key);
            }

            if (!key.StartsWith(_namespaceAndDot))
            {
                return null;
            }

            key = key.Substring(_namespaceAndDot.Length);
            return _valueProvider.GetValue(key);
        }
    }
}
