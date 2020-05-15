using System;
using System.Collections.Generic;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang
{
    public class LanguageNotSupported : Exception
    {
        public Assembly Assembly { get; }
        public string Language { get; }
        public IEnumerable<string> SupportedLanguages { get; }

        public LanguageNotSupported(Assembly assembly, string language, IEnumerable<string> supportedLanguages) =>
            (Assembly, Language, SupportedLanguages) = (assembly, language, supportedLanguages);
    }
}
