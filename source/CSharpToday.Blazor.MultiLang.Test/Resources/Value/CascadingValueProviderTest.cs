using CSharpToday.Blazor.MultiLang.Resources.Value;
using LucidCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using System.Collections.Generic;

namespace CSharpToday.Blazor.MultiLang.Test.Resources.Value
{
    [TestClass]
    public class CascadingValueProviderTest : BaseTest
    {
        [DataTestMethod]
        [DataRow(3)]
        [DataRow(10)]
        [DataRow(100)]
        public void Get_Value_When_Many_Providers(int providerCount) => LucidTest
            .DefineExpected("value")
            .Arrange(value =>
            {
                var providers = new List<IValueProvider>();
                for (int i = 1; i < providerCount; i++)
                {
                    providers.Add(new JsonValueProvider("{ }"));
                }

                const string Key = "key";
                var provider = Get<IValueProvider>();
                provider.GetValue(Key).Returns(value);
                providers.Add(provider);

                return new
                {
                    Key,
                    Provider = new CascadingValueProvider(providers)
                };
            })
            .Act(param => param.Provider.GetValue(param.Key))
            .Assert(ValuesAreTheSame);

        [TestMethod]
        public void Get_Value_When_Single_Provider() => LucidTest
            .DefineExpected("value")
            .Arrange(value =>
            {
                const string Key = "key";
                var provider = Get<IValueProvider>();
                provider.GetValue(Key).Returns(value);

                return new
                {
                    Key,
                    Provider = new CascadingValueProvider(provider)
                };
            })
            .Act(param => param.Provider.GetValue(param.Key))
            .Assert(ValuesAreTheSame);

        [TestMethod]
        public void No_Value_When_No_Provider() => LucidTest
            .Arrange(() => new CascadingValueProvider())
            .Act(provider => provider.GetValue("key"))
            .Assert(value => value.ShouldBeNull());
    }
}
