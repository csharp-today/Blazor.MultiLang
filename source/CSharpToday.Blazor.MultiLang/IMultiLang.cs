using System.Collections.Generic;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang
{
    public interface IMultiLang
    {
        ILanguage GetLanguage(Assembly assembly, string language);
        IEnumerable<string> GetSupportedLanguages(Assembly assembly);
    }
}
