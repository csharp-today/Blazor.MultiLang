using CSharpToday.Blazor.MultiLang;
using CSharpToday.Blazor.MultiLang.Resources;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static IServiceCollection AddBlazorMultiLang(this IServiceCollection services) => services
            .AddResources()
            .AddPublicItems();

        private static IServiceCollection AddPublicItems(this IServiceCollection services) =>
            services.Add<IMultiLang, MultiLang>();
    }
}
