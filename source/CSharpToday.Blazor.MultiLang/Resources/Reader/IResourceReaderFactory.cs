using System.Reflection;

namespace CSharpToday.Blazor.MultiLang.Resources.Reader
{
    internal interface IResourceReaderFactory
    {
        IResourceReader GetResourceReader(Assembly assembly);
    }
}
