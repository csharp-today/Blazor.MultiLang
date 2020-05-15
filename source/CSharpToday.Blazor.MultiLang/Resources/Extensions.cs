using CSharpToday.Blazor.MultiLang.Resources.Group;
using CSharpToday.Blazor.MultiLang.Resources.Reader;
using CSharpToday.Blazor.MultiLang.Resources.Tree;
using CSharpToday.Blazor.MultiLang.Resources.Value;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpToday.Blazor.MultiLang.Resources
{
    internal static class Extensions
    {
        public static IServiceCollection AddResources(this IServiceCollection services) => services
            .Add<IGroupConverter, GroupConverter>()
            .Add<IJsonValueProviderFactory, JsonValueProviderFactory>()
            .Add<IResourceGroupFactory, ResourceGroupFactory>()
            .Add<IResourceReader, ResourceReader>()
            .Add<IResourceReaderFactory, ResourceReaderFactory>()
            .Add<IResourceTreeBuilder, ResourceTreeBuilder>()
            .Add<IResourceValueProviderFactory, ResourceValueProviderFactory>();
    }
}
