using System.Collections.Generic;

namespace CSharpToday.Blazor.MultiLang.Resources.Tree
{
    internal interface IResourceTreeBuilder
    {
        IResourceTree BuildTree(IEnumerable<string> resources);
    }
}
