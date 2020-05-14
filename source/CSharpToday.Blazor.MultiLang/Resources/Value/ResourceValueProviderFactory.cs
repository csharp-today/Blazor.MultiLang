using CSharpToday.Blazor.MultiLang.Resources.Tree;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang.Resources.Value
{
    internal class ResourceValueProviderFactory : IResourceValueProviderFactory
    {
        private readonly IJsonValueProviderFactory _jsonValueProviderFactory;
        private readonly IResourceReaderFactory _resourceReaderFactory;

        public ResourceValueProviderFactory(IJsonValueProviderFactory jsonValueProviderFactory, IResourceReaderFactory resourceReaderFactory) =>
            (_jsonValueProviderFactory, _resourceReaderFactory) = (jsonValueProviderFactory, resourceReaderFactory);

        public IValueProvider GetResrouceValueProvider(Assembly assembly, IResourceTree resource) => new ResourceValueProvider(
            _jsonValueProviderFactory,
            _resourceReaderFactory.GetResourceReader(assembly),
            resource);
    }
}
