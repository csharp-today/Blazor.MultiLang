using CSharpToday.Blazor.MultiLang.Resources.Tree;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang.Resources.Value
{
    public interface IResourceValueProviderFactory
    {
        IValueProvider GetResrouceValueProvider(Assembly assembly, IResourceTree resource);
    }
}
