using CSharpToday.Blazor.MultiLang.Resources.Group;
using CSharpToday.Blazor.MultiLang.Resources.Value;
using System;
using System.Globalization;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang
{
    internal class Language : ILanguage
    {
        private readonly IResourceGroup _group;
        private readonly Lazy<IValueProvider> _valueProvider;

        public Assembly Assembly { get; }

        public string Code => _group.Name;

        public CultureInfo Culture => new CultureInfo(Code);

        public Language(IGroupConverter groupConverter, Assembly assembly, IResourceGroup group) =>
            (Assembly, _group, _valueProvider) =
            (assembly, group, new Lazy<IValueProvider>(() => groupConverter.ConvertToValueProvider(assembly, group)));

        public string GetTranslation(string name) => _valueProvider.Value.GetValue(name);

        public ITypedLanguage GetTypedLanguage(string type) => new TypedLanguage(this, type);
        public ITypedLanguage GetTypedLanguage<T>() => GetTypedLanguage(typeof(T).FullName);
        public ITypedLanguage GetTypedLanguage<T>(T obj) => GetTypedLanguage<T>();
    }
}
