using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang
{
    public interface IMultiLang
    {
        IEnumerable<string> GetSupportedLanguages(Assembly assembly);
    }
}
