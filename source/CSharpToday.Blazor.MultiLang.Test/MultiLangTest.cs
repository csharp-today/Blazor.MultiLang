using CSharpToday.Blazor.MultiLang.Resources.Group;
using LucidCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace CSharpToday.Blazor.MultiLang.Test
{
    [TestClass]
    public class MultiLangTest : BaseTest
    {
        private const string ValidExtension = ".json";

        [TestMethod]
        public void Get_Only_JSON_Files() => LucidTest
            .DefineExpected("pl")
            .Arrange(lang =>
            {
                var group1 = Substitute.For<IResourceGroup>();
                group1.Name.Returns(lang);
                group1.Extension.Returns(ValidExtension);

                var group2 = Substitute.For<IResourceGroup>();
                group2.Name.Returns("some");
                group2.Extension.Returns(".txt");

                Get<IResourceGroupFactory>().GetResourceGroups(null).ReturnsForAnyArgs(new[] { group1, group2 });
            })
            .Act(GetSupportedLanguages)
            .Assert((expectedLanguage, supportedLanguages) =>
            {
                supportedLanguages.Count().ShouldBe(1);
                supportedLanguages.First().ShouldBe(expectedLanguage);
            });

        [TestMethod]
        public void Language_From_ResourceGroup() => LucidTest
            .DefineExpected("pl")
            .Arrange(lang =>
            {
                var group = Get<IResourceGroup>();
                group.Name.Returns(lang);
                group.Extension.Returns(ValidExtension);
                Get<IResourceGroupFactory>().GetResourceGroups(null).ReturnsForAnyArgs(new[] { group });
            })
            .Act(GetSupportedLanguages)
            .Assert((expectedLanguage, supportedLanguages) =>
            {
                supportedLanguages.Count().ShouldBe(1);
                supportedLanguages.First().ShouldBe(expectedLanguage);
            });

        private IEnumerable<string> GetSupportedLanguages() =>
            Get<MultiLang>().GetSupportedLanguages(this.GetType().Assembly);
    }
}
