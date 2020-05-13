using CSharpToday.Blazor.MultiLang.Resources.Tree;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpToday.Blazor.MultiLang.Resources
{
    internal static class Extensions
    {
        public static IServiceCollection AddResources(this IServiceCollection services) => services
            .AddTransient<IResourceReader, ResourceReader>()
            .AddTransient<IResourceReaderFactory, ResourceReaderFactory>()
            .AddTransient<IResourceTreeBuilder, ResourceTreeBuilder>();
    }
}
