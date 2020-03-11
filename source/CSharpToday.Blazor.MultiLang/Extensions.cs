using CSharpToday.Blazor.MultiLang.Resources;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static IServiceCollection AddBlazorMultiLang(this IServiceCollection services) =>
            services.AddResources();
    }
}
