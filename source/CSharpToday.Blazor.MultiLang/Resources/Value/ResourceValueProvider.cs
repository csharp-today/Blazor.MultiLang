using CSharpToday.Blazor.MultiLang.Resources.Reader;
using CSharpToday.Blazor.MultiLang.Resources.Tree;
using System;

namespace CSharpToday.Blazor.MultiLang.Resources.Value
{
    internal class ResourceValueProvider : IValueProvider
    {
        private readonly Lazy<IValueProvider> _valueProvider;


        public ResourceValueProvider(IJsonValueProviderFactory jsonValueProviderFactory, IResourceReader resourceReader, IResourceTree resource)
        {
            _valueProvider = new Lazy<IValueProvider>(() =>
            {
                var json = resourceReader.GetResourceContext(resource.FullName);
                return jsonValueProviderFactory.GetJsonValueProvider(json);
            });
        }

        public string GetValue(string key) => _valueProvider.Value.GetValue(key);
    }
}
