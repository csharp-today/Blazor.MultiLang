using System.Collections.Generic;

namespace CSharpToday.Blazor.MultiLang.Resources.Reader
{
    public interface IResourceReader
    {
        string GetResourceContext(string resourcePath);
        IEnumerable<string> GetResources();
    }
}
