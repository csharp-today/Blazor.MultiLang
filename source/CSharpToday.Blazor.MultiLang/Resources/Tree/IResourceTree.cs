using System.Collections.Generic;

namespace CSharpToday.Blazor.MultiLang.Resources.Tree
{
    internal interface IResourceTree
    {
        IEnumerable<IResourceTree> Children { get; }
        string FullName { get; }
        string Name { get; }
        string Namespace { get; }
        IResourceTree Parent { get; }
    }
}
