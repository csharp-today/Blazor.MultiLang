using CSharpToday.Blazor.MultiLang.Resources.Tree;

namespace CSharpToday.Blazor.MultiLang.Resources.Group
{
    public interface IResourceGroup
    {
        public string Extension { get; }
        public string FullName { get; }
        public string Name { get; }
        public IResourceTree Tree { get; }
    }
}
