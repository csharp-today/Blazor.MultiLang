using System.Reflection;

namespace CSharpToday.Blazor.MultiLang.Resources
{
    public interface IResourceReaderFactory
    {
        IResourceReader GetResourceReader(Assembly assembly);
    }
}
