using System.Collections.Generic;
using System.Linq;

namespace CSharpToday.Blazor.MultiLang.Resources.Tree
{
    internal class ResourceTreeBuilder : IResourceTreeBuilder
    {
        private class TempNode
        {
            public List<TempNode> Children { get; set; } = new List<TempNode>();
            public string[] Namespaces { get; set; }
            public string ResourcePath { get; set; }
        }

        public IResourceTree BuildTree(IEnumerable<string> resources)
        {
            if (resources is null || !resources.Any())
            {
                return null;
            }

            var tempNodes = resources
                .Select(r => new TempNode
                {
                    Namespaces = new ResourceTree(r).Namespace.Split('.'),
                    ResourcePath = r
                })
                .OrderBy(item => item.Namespaces.Length);

            var roots = new List<TempNode>();
            foreach (var node in tempNodes)
            {
                AddNode(roots, node);
            }

            var trees = roots.Select(r => ConvertToTree(r));
            if (trees.Count() == 1)
            {
                return trees.First();
            }

            return new ResourceTree("", trees);
        }

        private void AddNode(List<TempNode> children, TempNode node)
        {
            foreach (var root in children)
            {
                if (IsChild(root, node))
                {
                    AddNode(root.Children, node);
                    return;
                }
            }

            children.Add(node);
        }

        private IResourceTree ConvertToTree(TempNode node) => new ResourceTree(
            node.ResourcePath,
            node.Children.Select(n => ConvertToTree(n)));

        private bool IsChild(TempNode parent, TempNode child)
        {
            if (parent.Namespaces.Length >= child.Namespaces.Length)
            {
                return false;
            }

            for (int idx = 0; idx < parent.Namespaces.Length; idx++)
            {
                if (parent.Namespaces[idx] != child.Namespaces[idx])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
