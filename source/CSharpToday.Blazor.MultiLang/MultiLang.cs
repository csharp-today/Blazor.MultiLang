using CSharpToday.Blazor.MultiLang.Resources.Group;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang
{
    internal class MultiLang : IMultiLang
    {
        private static readonly Lazy<string[]> _allLanguages = new Lazy<string[]>(() => CultureInfo
            .GetCultures(CultureTypes.AllCultures)
            .Select(ci => ci.Name)
            .ToArray());

        private readonly Dictionary<Assembly, IResourceGroup[]> _groupCache = new Dictionary<Assembly, IResourceGroup[]>();
        private readonly IResourceGroupFactory _groupFactory;
        private readonly ILanguageFactory _languageFactory;

        public MultiLang(IResourceGroupFactory groupFactory, ILanguageFactory languageFactory) =>
            (_groupFactory, _languageFactory) = (groupFactory, languageFactory);

        public ILanguage GetLanguage(Assembly assembly, string language)
        {
            var supportedLanguages = GetSupportedLanguages(assembly);
            if (!supportedLanguages.Contains(language))
            {
                throw new LanguageNotSupported(assembly, language, supportedLanguages);
            }

            var group = GetGroups(assembly).First(g => g.Name == language);
            var lang = _languageFactory.GetLanguage(assembly, group);
            return lang;
        }

        public IEnumerable<string> GetSupportedLanguages(Assembly assembly) =>
            GetGroups(assembly)
            .Where(g => IsLanguageGroup(g))
            .Select(g => g.Name)
            .ToArray();

        private IResourceGroup[] GetGroups(Assembly assembly)
        {
            if (_groupCache.ContainsKey(assembly))
            {
                return _groupCache[assembly];
            }

            var groups = _groupFactory.GetResourceGroups(assembly).ToArray();
            _groupCache.Add(assembly, groups);
            return groups;
        }

        private bool IsLanguageGroup(IResourceGroup group) =>
            group.Extension == ".json"
            && _allLanguages.Value.Contains(group.Name);
    }
}
