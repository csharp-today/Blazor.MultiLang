using CSharpToday.Blazor.MultiLang.Resources.Group;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang
{
    internal interface ILanguageFactory
    {
        ILanguage GetLanguage(Assembly assembly, IResourceGroup group);
    }
}
