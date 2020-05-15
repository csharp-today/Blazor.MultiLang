using System.Collections.Generic;

namespace CSharpToday.Blazor.MultiLang.Resources.Reader
{
    internal interface IResourceReader
    {
        string GetResourceContext(string resourcePath);
        IEnumerable<string> GetResources();
    }
}
