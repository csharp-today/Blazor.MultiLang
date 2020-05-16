using CSharpToday.Blazor.MultiLang.Resources.Group;
using CSharpToday.Blazor.MultiLang.Resources.Value;
using LucidCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang.Test
{
    [TestClass]
    public class LanguageTest : BaseTest
    {
        private Assembly Assembly => GetType().Assembly;

        [TestMethod]
        public void Get_Assembly() => LucidTest
            .DefineExpected(Assembly)
            .Arrange(asm => GetLanguage(asm, null))
            .Act(lang => lang.Assembly)
            .Assert(ValuesAreTheSame);

        [TestMethod]
        public void Get_CultureInfo() => LucidTest
            .DefineExpected("pl")
            .Arrange(code => (ILanguage)GetLanguage(GetType().Assembly, GetGroup(code)))
            .Act(lang => lang.Culture)
            .Assert((expectedLanguage, culture) => culture.Name.ShouldBe(expectedLanguage));

        [TestMethod]
        public void Get_Value_From_Group_Value_Provider() => LucidTest
            .DefineExpected("value")
            .Arrange(code =>
            {
                var valueProvider = Get<IValueProvider>();
                const string Key = "key";
                valueProvider.GetValue(Key).Returns(code);

                var asm = GetType().Assembly;
                var group = Get<IResourceGroup>();
                var converter = Get<IGroupConverter>();
                converter.ConvertToValueProvider(asm, group).Returns(valueProvider);

                return new
                {
                    Assembly = asm,
                    Code = code,
                    Converter = converter,
                    Key
                };
            })
            .Act(param => GetLanguage(param.Assembly, GetGroup(param.Code), param.Converter).GetTranslation(param.Key))
            .Assert(ValuesAreTheSame);

        private IResourceGroup GetGroup(string name)
        {
            var group = Get<IResourceGroup>();
            group.Name.Returns(name);
            return group;
        }

        private Language GetLanguage(Assembly asm, IResourceGroup group, IGroupConverter converter = null) =>
            new Language(converter, asm, group);
    }
}
