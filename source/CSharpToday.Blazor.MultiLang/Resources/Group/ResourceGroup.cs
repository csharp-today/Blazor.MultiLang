using CSharpToday.Blazor.MultiLang.Resources.Tree;

namespace CSharpToday.Blazor.MultiLang.Resources.Group
{
    internal class ResourceGroup : IResourceGroup
    {
        public string Extension { get; set; }

        public string FullName { get; set; }

        public string Name { get; set; }

        public IResourceTree Tree { get; set; }
    }
}
