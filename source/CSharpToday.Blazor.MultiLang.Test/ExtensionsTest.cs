using CSharpToday.Blazor.MultiLang.Resources;
using CSharpToday.Blazor.MultiLang.Resources.Group;
using CSharpToday.Blazor.MultiLang.Resources.Reader;
using CSharpToday.Blazor.MultiLang.Resources.Tree;
using CSharpToday.Blazor.MultiLang.Resources.Value;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;

namespace CSharpToday.Blazor.MultiLang.Test
{
    [TestClass]
    public class ExtensionsTest : BaseTest
    {
        private readonly IServiceProvider _provider;

        public ExtensionsTest() => _provider = new ServiceCollection().AddResources().BuildServiceProvider();

        [TestMethod]
        public void Create_IGroupConverter() => Validate<IGroupConverter>();

        [TestMethod]
        public void Create_IJsonValueProviderFactory() => Validate<IJsonValueProviderFactory>();

        [TestMethod]
        public void Create_IResourceGroupFactory() => Validate<IResourceGroupFactory>();

        [TestMethod]
        public void Create_IResourceReaderFactory() => Validate<IResourceReaderFactory>();

        [TestMethod]
        public void Create_IResourceTreeBuilder() => Validate<IResourceTreeBuilder>();

        [TestMethod]
        public void Create_IResourceValueProviderFactory() => Validate<IResourceValueProviderFactory>();

        private void Validate<T>() => _provider.GetService<T>().ShouldNotBeNull();
    }
}
