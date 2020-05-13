using System.Collections.Generic;

namespace CSharpToday.Blazor.MultiLang.Resources.Tree
{
    public interface IResourceTreeBuilder
    {
        IResourceTree BuildTree(IEnumerable<string> resources);
    }
}
