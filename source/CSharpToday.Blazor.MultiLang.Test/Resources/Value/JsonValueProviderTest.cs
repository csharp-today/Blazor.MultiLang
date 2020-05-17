using CSharpToday.Blazor.MultiLang.Resources.Value;
using LucidCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace CSharpToday.Blazor.MultiLang.Test.Resources.Value
{
    [TestClass]
    public class JsonValueProviderTest
    {
        [TestMethod]
        public void Get_Null_When_Json_Is_Null() => LucidTest
            .Act(() => new JsonValueProvider(null).GetValue("any"))
            .Assert(value => value.ShouldBeNull());

        [DataTestMethod]
        [DataRow("SimpleName")]
        [DataRow("Advanced.Property.Name")]
        public void Get_Property_Value(string propertyName) => LucidTest
            .DefineExpected(() => "ExpectedValue")
            .Arrange(value => new
            {
                Name = propertyName,
                Provider = (IValueProvider)new JsonValueProvider($"{{ \"{propertyName}\": \"{value}\" }}")
            })
            .Act(param => param.Provider.GetValue(param.Name))
            .Assert((expectedValue, value) => value.ShouldBe(expectedValue));

        [DataTestMethod]
        [DataRow("MasterName", "SubName")]
        [DataRow("MasterName.More.Names", "SubName")]
        [DataRow("MasterName", "SubName.More.Names")]
        [DataRow("MasterName.Name1", "SubName.Name2")]
        public void Get_SubValue(string name1, string name2) => LucidTest
            .DefineExpected(() => "ExpectedValue")
            .Arrange(value =>
            {
                var json = $"{{ \"{name1}\": {{ \"{name2}\": \"{value}\" }} }}";
                return new
                {
                    Name = $"{name1}.{name2}",
                    Provider = (IValueProvider)new JsonValueProvider(json)
                };
            })
            .Act(param => param.Provider.GetValue(param.Name))
            .Assert((expectedValue, value) => value.ShouldBe(expectedValue));
    }
}
