using CSharpToday.Blazor.MultiLang.Resources.Tree;
using LucidCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Linq;

namespace CSharpToday.Blazor.MultiLang.Test.Resources.Tree
{
    [TestClass]
    public class ResourceTreeTest
    {
        [TestMethod]
        public void Child_Has_Parent_Set() => LucidTest
            .Arrange(() =>
            {
                IResourceTree child = new ResourceTree("child");
                IResourceTree parent = new ResourceTree("parent", new[] { child });
                return (child, parent);
            })
            .Act(items => (expectedParent: items.parent, items.child.Parent))
            .Assert(items => items.Parent.ShouldBe(items.expectedParent));

        [TestMethod]
        public void Children_Are_Present() => LucidTest
            .DefineExpected(() => new IResourceTree[]
            {
                new ResourceTree("Child1"),
                new ResourceTree("Child2")
            })
            .Arrange(children => (IResourceTree)new ResourceTree("Parent", children))
            .Act(tree => tree.Children)
            .Assert((expectedChildren, actualChildren) =>
            {
                actualChildren.Count().ShouldBe(expectedChildren.Length);
                foreach (var expectedChild in expectedChildren)
                {
                    actualChildren.Contains(expectedChild);
                }
            });

        [TestMethod]
        public void FullName_Returns_Proper_Value() => LucidTest
            .DefineExpected(() => "some.path.to.resource")
            .Arrange(path => (IResourceTree)new ResourceTree(path))
            .Act(tree => tree.FullName)
            .Assert((expectedPath, fullPath) => fullPath.ShouldBe(expectedPath));

        [TestMethod]
        public void Name_Returns_Proper_Value() => LucidTest
            .DefineExpected(() => "my_file.json")
            .Arrange(name => (IResourceTree)new ResourceTree($"some.namespace.{name}"))
            .Act(tree => tree.Name)
            .Assert((expectedName, name) => name.ShouldBe(expectedName));

        [TestMethod]
        public void Namespace_Returns_Proper_Value() => LucidTest
            .DefineExpected(() => "Expected.Namespace.Name")
            .Arrange(namespeceName => (IResourceTree)new ResourceTree($"{namespeceName}.MyFile.json"))
            .Act(tree => tree.Namespace)
            .Assert((expectedNamespace, actualNamespace) => actualNamespace.ShouldBe(expectedNamespace));

        [TestMethod]
        public void Parent_Is_Null_When_Tree_Is_Root() => LucidTest
            .Arrange(() => (IResourceTree)new ResourceTree("name"))
            .Act(tree => tree.Parent)
            .Assert(parentTree => parentTree.ShouldBeNull());
    }
}
