using System.Globalization;

namespace CSharpToday.Blazor.MultiLang
{
    public interface ILanguage
    {
        string Code { get; }
        CultureInfo Culture { get; }
        string GetTranslation(string name);
        ITypedLanguage GetTypedLanguage(string type);
        ITypedLanguage GetTypedLanguage<T>();
        ITypedLanguage GetTypedLanguage<T>(T obj);
    }
}
