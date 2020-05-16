using System.Globalization;

namespace CSharpToday.Blazor.MultiLang
{
    internal class TypedLanguage : ITypedLanguage
    {
        private readonly ILanguage _language;

        public string TypePrefix { get; }

        public string Code => _language.Code;

        public CultureInfo Culture => _language.Culture;

        public TypedLanguage(ILanguage language, string typePrefix) =>
            (_language, TypePrefix) = (language, typePrefix);

        public string GetTranslation(string name) => _language.GetTranslation($"{TypePrefix}.{name}");

        public ITypedLanguage GetTypedLanguage(string type) => new TypedLanguage(this, type);
        public ITypedLanguage GetTypedLanguage<T>() => GetTypedLanguage(typeof(T).FullName);
        public ITypedLanguage GetTypedLanguage<T>(T obj) => GetTypedLanguage<T>();
    }
}
