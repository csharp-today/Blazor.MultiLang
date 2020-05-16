using LucidCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Globalization;

namespace CSharpToday.Blazor.MultiLang.Test
{
    [TestClass]
    public class TypedLanguageTest : BaseTest
    {
        [TestMethod]
        public void Return_The_Same_Code() => LucidTest
            .DefineExpected("code")
            .Arrange(code => Get<ILanguage>().Set(l => l.Code.Returns(code)))
            .Act(lang => new TypedLanguage(lang, "").Code)
            .Assert(ValuesAreTheSame);

        [TestMethod]
        public void Return_The_Same_Culture() => LucidTest
            .DefineExpected(new CultureInfo("pl"))
            .Arrange(culture => Get<ILanguage>().Set(l => l.Culture.Returns(culture)))
            .Act(lang => new TypedLanguage(lang, "").Culture)
            .Assert(ValuesAreTheSame);

        [TestMethod]
        public void TypePrefix_Is_Set() => LucidTest
            .DefineExpected("Type.Prefix")
            .Arrange(p => p)
            .Act(typePrefix => new TypedLanguage(null, typePrefix).TypePrefix)
            .Assert(ValuesAreTheSame);

        [TestMethod]
        public void Use_TypePrefix_To_Get_Translation() => LucidTest
            .DefineExpected("value")
            .Arrange(value =>
            {
                const string Prefix = "Type.Prefix", Key = "Key";
                var lang = Get<ILanguage>().Set(l => l.GetTranslation($"{Prefix}.{Key}").Returns(value));
                return new
                {
                    Key,
                    TypedLanguage = new TypedLanguage(lang, Prefix)
                };
            })
            .Act(param => param.TypedLanguage.GetTranslation(param.Key))
            .Assert(ValuesAreTheSame);
    }
}
