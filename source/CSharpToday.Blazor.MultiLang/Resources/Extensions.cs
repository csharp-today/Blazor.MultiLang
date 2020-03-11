using Microsoft.Extensions.DependencyInjection;

namespace CSharpToday.Blazor.MultiLang.Resources
{
    internal static class Extensions
    {
        public static IServiceCollection AddResources(this IServiceCollection services) =>
            services.AddTransient<IResourceReader, ResourceReader>();
    }
}
