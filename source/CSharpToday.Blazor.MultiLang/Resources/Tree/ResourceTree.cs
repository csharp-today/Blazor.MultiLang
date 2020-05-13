using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSharpToday.Blazor.MultiLang.Resources.Tree
{
    internal class ResourceTree : IResourceTree
    {
        public IEnumerable<IResourceTree> Children { get; }
        public string FullName { get; }
        public string Name => Namespace.Length > 0
            ? FullName.Replace(Namespace, "").TrimStart('.')
            : FullName;
        public string Namespace => SeparatorIndex >= 0 ? FullName.Substring(0, SeparatorIndex) : FullName;
        public IResourceTree Parent { get; private set; }

        private string NameExtension => Path.GetExtension(FullName);
        private int SeparatorIndex => NameExtension.Length > 0
            ? FullName.Replace(NameExtension, "").LastIndexOf('.')
            : -1;

        public ResourceTree(string resourcePath, params IResourceTree[] children)
            : this(resourcePath, children.AsEnumerable()) { }

        public ResourceTree(string resourcePath, IEnumerable<IResourceTree> children)
        {
            (FullName, Children) = (resourcePath, children);
            foreach (var child in Children)
            {
                if (child is ResourceTree tree)
                {
                    tree.Parent = this;
                }
            }
        }
    }
}
