using CSharpToday.Blazor.MultiLang.Resources.Group;
using LucidCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang.Test
{
    [TestClass]
    public class MultiLangTest : BaseTest
    {
        private Assembly Assembly => GetType().Assembly;

        [TestMethod]
        public void Get_Language() => LucidTest
            .DefineExpected("pl")
            .Arrange(CreateLanguageAndPassCode)
            .Act(GetLanguage)
            .Assert((expectedLanguage, language) => language.Code.ShouldBe(expectedLanguage));

        [TestMethod]
        public void Get_Only_JSON_Files() => LucidTest
            .DefineExpected("pl")
            .Arrange(lang => SetGroups(GetValidGroup(lang), GetGroup("some", ".txt")))
            .Act(GetSupportedLanguages)
            .Assert((expectedLanguage, supportedLanguages) =>
            {
                supportedLanguages.Count().ShouldBe(1);
                supportedLanguages.First().ShouldBe(expectedLanguage);
            });

        [TestMethod]
        public void Language_From_ResourceGroup() => LucidTest
            .DefineExpected("pl")
            .Arrange(lang => SetGroups(GetValidGroup(lang)))
            .Act(GetSupportedLanguages)
            .Assert((expectedLanguage, supportedLanguages) =>
            {
                supportedLanguages.Count().ShouldBe(1);
                supportedLanguages.First().ShouldBe(expectedLanguage);
            });

        [TestMethod]
        public void Throw_Exception_When_Not_Supported_Language() => LucidTest
            .DefineExpected("xyz")
            .Arrange(param => param)
            .Act(GetLanguageException)
            .Assert((language, exception) =>
            {
                exception.ShouldBeOfType<LanguageNotSupported>();

                var notSupportedEx = (LanguageNotSupported)exception;
                notSupportedEx.Assembly.ShouldBe(Assembly);
                notSupportedEx.Language.ShouldBe(language);
                notSupportedEx.SupportedLanguages.ShouldNotBeNull();
                notSupportedEx.SupportedLanguages.Count().ShouldBe(0);
            });

        [TestMethod]
        public void Throw_Exception_With_Supported_Languages() => LucidTest
            .DefineExpected(new[] { "pl", "en" })
            .Arrange(languages =>
            {
                var groups = new List<IResourceGroup>();
                foreach (var lang in languages)
                {
                    groups.Add(GetValidGroup(lang));
                }
                SetGroups(groups.ToArray());
            })
            .Act(() => (LanguageNotSupported)GetLanguageException("otherLanguage"))
            .Assert((expectedLanguages, exception) =>
            {
                foreach (var language in expectedLanguages)
                {
                    exception.SupportedLanguages.ShouldContain(language);
                }
            });

        private string CreateLanguageAndPassCode(string language)
        {
            var group = GetValidGroup(language);
            SetGroups(group);

            var lang = Get<ILanguage>();
            lang.Code.Returns(language);
            Get<ILanguageFactory>().GetLanguage(Assembly, group).Returns(lang);

            return language;
        }

        private IResourceGroup GetGroup(string name, string extension)
        {
            var group = Substitute.For<IResourceGroup>();
            group.Name.Returns(name);
            group.Extension.Returns(extension);
            return group;
        }

        private ILanguage GetLanguage(string language) =>
            Get<MultiLang>().GetLanguage(Assembly, language);

        private Exception GetLanguageException(string language)
        {
            try
            {
                GetLanguage(language);
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        private IEnumerable<string> GetSupportedLanguages() =>
            Get<MultiLang>().GetSupportedLanguages(Assembly);

        private IResourceGroup GetValidGroup(string name) => GetGroup(name, ".json");

        private void SetGroups(params IResourceGroup[] groups) =>
            Get<IResourceGroupFactory>().GetResourceGroups(Assembly).Returns(groups);
    }
}
