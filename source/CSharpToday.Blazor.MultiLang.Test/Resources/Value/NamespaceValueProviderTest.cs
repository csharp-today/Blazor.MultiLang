using CSharpToday.Blazor.MultiLang.Resources.Value;
using LucidCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;

namespace CSharpToday.Blazor.MultiLang.Test.Resources.Value
{
    [TestClass]
    public class NamespaceValueProviderTest : BaseTest
    {
        [TestMethod]
        public void Get_Value_With_Namespace() => LucidTest
            .DefineExpected("value")
            .Arrange(value =>
            {
                const string Namespace = "Namespace", Key = "Key";

                var valueProvider = Get<IValueProvider>();
                valueProvider.GetValue(Key).Returns(value);

                return new
                {
                    Key = $"{Namespace}.{Key}",
                    Provider = new NamespaceValueProvider(Namespace, valueProvider)
                };
            })
            .Act(param => param.Provider.GetValue(param.Key))
            .Assert(ValuesAreTheSame);

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void Get_Value_Without_Namespace(string namespaceName) => LucidTest
            .DefineExpected("value")
            .Arrange(value =>
            {
                var valueProvider = Get<IValueProvider>();

                const string Key = "name";
                valueProvider.GetValue(Key).Returns(value);

                return new
                {
                    Key,
                    Provider = new NamespaceValueProvider(namespaceName, valueProvider)
                };
            })
            .Act(param => param.Provider.GetValue(param.Key))
            .Assert(ValuesAreTheSame);

        [TestMethod]
        public void No_Value_When_Wrong_Namespace() => LucidTest
            .Arrange(() =>
            {
                const string Key = "key";
                var valueProvider = Get<IValueProvider>();
                valueProvider.GetValue(Key).Returns("value");

                return new
                {
                    Key = $"Wrong.Namespace.{Key}",
                    Provider = new NamespaceValueProvider("Correct.Namespace", valueProvider)
                };
            })
            .Act(param => param.Provider.GetValue(param.Key))
            .Assert(value => value.ShouldBeNull());
    }
}
