using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;

namespace CSharpToday.Blazor.MultiLang.Test
{
    public abstract class BaseExtensionTest
    {
        protected IServiceProvider Provider { get; }

        public BaseExtensionTest()
        {
            var services = new ServiceCollection();
            AddServices(services);
            Provider = services.BuildServiceProvider();
        }

        protected abstract void AddServices(IServiceCollection services);

        protected void Validate<T>() => Provider.GetService<T>().ShouldNotBeNull();
    }
}
