using CSharpToday.Blazor.MultiLang.Resources.Value;
using System.Globalization;

namespace CSharpToday.Blazor.MultiLang
{
    public interface ILanguage : IValueProvider
    {
        string Code { get; }
        CultureInfo Culture { get; }
    }
}
