using CSharpToday.Blazor.MultiLang.Resources.Tree;
using CSharpToday.Blazor.MultiLang.Resources.Value;
using System.Collections.Generic;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang.Resources.Group
{
    internal class GroupConverter : IGroupConverter
    {
        private readonly IResourceValueProviderFactory _resourceValueProviderFactory;

        public GroupConverter(IResourceValueProviderFactory resourceValueProviderFactory) =>
            (_resourceValueProviderFactory) = (resourceValueProviderFactory);

        public IValueProvider ConvertToValueProvider(Assembly assembly, IResourceGroup group)
        {
            return ConvertTree(assembly, group.Tree);
        }

        private IValueProvider ConvertTree(Assembly assembly, IResourceTree tree)
        {
            var treeValueProvider = _resourceValueProviderFactory.GetResrouceValueProvider(assembly, tree);
            var namespaceValueProvider = new NamespaceValueProvider(tree.Namespace, treeValueProvider);

            var providers = new List<IValueProvider> { namespaceValueProvider };
            foreach (var child in tree.Children)
            {
                var provider = ConvertTree(assembly, child);
                providers.Add(provider);
            }

            var cascadingProvder = new CascadingValueProvider(providers);
            return cascadingProvder;
        }
    }
}
