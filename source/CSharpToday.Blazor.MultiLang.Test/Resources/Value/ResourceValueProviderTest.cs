using CSharpToday.Blazor.MultiLang.Resources;
using CSharpToday.Blazor.MultiLang.Resources.Reader;
using CSharpToday.Blazor.MultiLang.Resources.Tree;
using CSharpToday.Blazor.MultiLang.Resources.Value;
using LucidCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;

namespace CSharpToday.Blazor.MultiLang.Test.Resources.Value
{
    [TestClass]
    public class ResourceValueProviderTest : BaseTest
    {
        [TestMethod]
        public void Pass_Resource_Content_To_JsonValueProvider() => LucidTest
            .DefineExpected("JSON")
            .Arrange(json =>
            {
                Get<IResourceReader>().GetResourceContext(null).ReturnsForAnyArgs(json);
            })
            .Act(() => { Get<ResourceValueProvider>().GetValue(null); })
            .Assert(expectedContent =>
                Get<IJsonValueProviderFactory>()
                .Received()
                .GetJsonValueProvider(expectedContent));

        [TestMethod]
        public void Pass_Resource_Path_To_ResourceReader() => LucidTest
            .DefineExpected("path")
            .Arrange(path =>
            {
                Get<IResourceTree>().FullName.Returns(path);
            })
            .Act(() => { Get<ResourceValueProvider>().GetValue(null); })
            .Assert(expectedPath =>
                Get<IResourceReader>()
                .Received()
                .GetResourceContext(expectedPath));

        [TestMethod]
        public void Return_Value_From_JsonValueProvider() => LucidTest
            .DefineExpected(() => "value")
            .Arrange(value =>
            {
                var valueProvider = Get<IValueProvider>();
                Get<IJsonValueProviderFactory>().GetJsonValueProvider(null).ReturnsForAnyArgs(valueProvider);

                const string Key = "key";
                valueProvider.GetValue(Key).Returns(value);
                return Key;
            })
            .Act(key => Get<ResourceValueProvider>().GetValue(key))
            .Assert((expectedValue, value) => value.ShouldBe(expectedValue));
    }
}
