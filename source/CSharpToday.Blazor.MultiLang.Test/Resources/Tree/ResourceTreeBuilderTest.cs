using CSharpToday.Blazor.MultiLang.Resources.Tree;
using LucidCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace CSharpToday.Blazor.MultiLang.Test.Resources.Tree
{
    [TestClass]
    public class ResourceTreeBuilderTest
    {
        [TestMethod]
        public void Exclusive_Roots() => LucidTest
            .DefineExpected(() => new
            {
                Root1 = "Some.Root.Resource.json",
                Root2 = "Other.Root.Resource.json"
            })
            .Arrange(item => new[] { item.Root1, item.Root2 })
            .Act(GetResourceTree)
            .Assert((expected, tree) =>
            {
                tree.Name.ShouldBe("");
                tree.Children.ShouldContain(t => t.FullName == expected.Root1);
                tree.Children.ShouldContain(t => t.FullName == expected.Root2);
            });

        [DataTestMethod]
        [DataRow(null)]
        [DataRow(new string[0])]
        public void Return_Null_When_No_Resources(string[] resources) => LucidTest
            .Act(() => GetResourceTree(resources))
            .Assert(tree => tree.ShouldBeNull());

        [TestMethod]
        public void Root_With_Children() => LucidTest
            .DefineExpected(() =>
            {
                const string ParentNamespace = "My.Parent.Namespace.";
                var childNamespace = $"{ParentNamespace}SubName.";
                return new
                {
                    Parent = $"{ParentNamespace}Parent.json",
                    Child1 = $"{childNamespace}Child1.json",
                    Child2 = $"{childNamespace}Child2.json"
                };
            })
            .Arrange(param => new[] { param.Child1, param.Parent, param.Child2 })
            .Act(GetResourceTree)
            .Assert((expected, tree) =>
            {
                tree.FullName.ShouldBe(expected.Parent);
                tree.Children.ShouldContain(n => n.FullName == expected.Child1);
                tree.Children.ShouldContain(n => n.FullName == expected.Child2);
            });

        [TestMethod]
        public void Root_With_Childs_Child() => LucidTest
            .DefineExpected(() =>
            {
                const string ParentNamespace = "Parent.Namespace.";
                var childNamespace = $"{ParentNamespace}Child.";
                return new
                {
                    Parent = $"{ParentNamespace}Parent.json",
                    Child = $"{childNamespace}Child.json",
                    SubChild = $"{childNamespace}.SubChild.File.json"
                };
            })
            .Arrange(param => new[] { param.Parent, param.SubChild, param.Child })
            .Act(GetResourceTree)
            .Assert((expected, tree) =>
            {
                tree.FullName.ShouldBe(expected.Parent);
                tree.Children.First().FullName.ShouldBe(expected.Child);
                tree.Children.First().Children.First().FullName.ShouldBe(expected.SubChild);
            });

        [TestMethod]
        public void Single_Root_For_One_Resource() => LucidTest
            .DefineExpected(() => "My.Resource.Name.json")
            .Arrange(name => new[] { name })
            .Act(GetResourceTree)
            .Assert((expectedName, tree) => tree.FullName.ShouldBe(expectedName));

        private IResourceTree GetResourceTree(IEnumerable<string> resources) =>
            new ResourceTreeBuilder().BuildTree(resources);
    }
}
