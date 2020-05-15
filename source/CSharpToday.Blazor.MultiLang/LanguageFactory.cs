using CSharpToday.Blazor.MultiLang.Resources.Group;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang
{
    internal class LanguageFactory : ILanguageFactory
    {
        private readonly IGroupConverter _groupConverter;

        public LanguageFactory(IGroupConverter groupConverter) => _groupConverter = groupConverter;

        public ILanguage GetLanguage(Assembly assembly, IResourceGroup group)
        {
            return new Language(_groupConverter, assembly, group);
        }
    }
}
