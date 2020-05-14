using System.Reflection;

namespace CSharpToday.Blazor.MultiLang.Resources.Reader
{
    internal class ResourceReaderFactory : IResourceReaderFactory
    {
        public IResourceReader GetResourceReader(Assembly assembly) => new ResourceReader(assembly);
    }
}
