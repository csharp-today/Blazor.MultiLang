using System.Reflection;

namespace CSharpToday.Blazor.MultiLang.Resources.Reader
{
    public interface IResourceReaderFactory
    {
        IResourceReader GetResourceReader(Assembly assembly);
    }
}
