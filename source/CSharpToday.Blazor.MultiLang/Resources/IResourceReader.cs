using System.Collections.Generic;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang.Resources
{
    public interface IResourceReader
    {
        IEnumerable<string> GetResources(Assembly assembly = null);
    }
}
