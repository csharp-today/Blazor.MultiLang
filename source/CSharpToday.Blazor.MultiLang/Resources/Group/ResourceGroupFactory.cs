using CSharpToday.Blazor.MultiLang.Resources.Reader;
using CSharpToday.Blazor.MultiLang.Resources.Tree;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang.Resources.Group
{
    internal class ResourceGroupFactory : IResourceGroupFactory
    {
        private readonly IResourceReaderFactory _resourceReaderFactory;
        private readonly IResourceTreeBuilder _resourceTreeBuilder;

        public ResourceGroupFactory(IResourceReaderFactory resourceReaderFactory, IResourceTreeBuilder resourceTreeBuilder) =>
            (_resourceReaderFactory, _resourceTreeBuilder) = (resourceReaderFactory, resourceTreeBuilder);

        public IEnumerable<IResourceGroup> GetResourceGroups(Assembly assembly)
        {
            var reader = _resourceReaderFactory.GetResourceReader(assembly);
            var resources = reader.GetResources();
            var groups = resources
                .Select(r => new ResourceTree(r))
                .GroupBy(tree => tree.Name)
                .Select(group => new ResourceGroup
                {
                    FullName = group.Key,
                    Extension = Path.GetExtension(group.Key),
                    Name = Path.GetFileNameWithoutExtension(group.Key),
                    Tree = _resourceTreeBuilder.BuildTree(group.Select(t => t.FullName))
                });
            return groups;
        }
    }
}
