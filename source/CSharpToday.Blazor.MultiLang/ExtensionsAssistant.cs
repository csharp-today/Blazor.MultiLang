using Microsoft.Extensions.DependencyInjection;

namespace CSharpToday.Blazor.MultiLang
{
    internal static class BaseExtensions
    {
        public static IServiceCollection Add<T1, T2>(this IServiceCollection services)
            where T1 : class
            where T2 : class, T1
            => services.AddTransient<T1, T2>();
    }
}
