using System.Collections.Generic;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang.Resources
{
    public interface IResourceReader
    {
        string GetResourceContext(string resourcePath);
        IEnumerable<string> GetResources();
    }
}
