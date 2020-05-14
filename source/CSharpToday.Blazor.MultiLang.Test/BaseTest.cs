using Ninject;
using Ninject.MockingKernel.NSubstitute;
using System;

namespace CSharpToday.Blazor.MultiLang.Test
{
    public class BaseTest : IDisposable
    {
        protected NSubstituteMockingKernel MockingKernel { get; } = new NSubstituteMockingKernel();

        protected T Get<T>() => MockingKernel.Get<T>();

        public void Dispose() => MockingKernel.Dispose();
    }
}
