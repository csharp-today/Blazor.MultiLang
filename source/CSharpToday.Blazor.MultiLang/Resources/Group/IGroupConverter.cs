using CSharpToday.Blazor.MultiLang.Resources.Value;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang.Resources.Group
{
    public interface IGroupConverter
    {
        IValueProvider ConvertToValueProvider(Assembly assembly, IResourceGroup group);
    }
}
