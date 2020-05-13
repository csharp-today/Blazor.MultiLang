using System.Collections.Generic;

namespace CSharpToday.Blazor.MultiLang.Resources.Tree
{
    public interface IResourceTree
    {
        IEnumerable<IResourceTree> Children { get; }
        string FullName { get; }
        string Name { get; }
        string Namespace { get; }
        IResourceTree Parent { get; }
    }
}
