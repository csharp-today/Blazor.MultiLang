using CSharpToday.Blazor.MultiLang.Resources.Group;
using CSharpToday.Blazor.MultiLang.Resources.Reader;
using CSharpToday.Blazor.MultiLang.Resources.Tree;
using LucidCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Shouldly;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSharpToday.Blazor.MultiLang.Test.Resources.Group
{
    [TestClass]
    public class ResourceGroupFactoryTest : BaseTest
    {
        public string[] _resources;

        public ResourceGroupFactoryTest()
        {
            MockingKernel.Bind<IResourceTreeBuilder>().To<ResourceTreeBuilder>();

            var reader = Get<IResourceReader>();
            reader.GetResources().Returns(_ => _resources);
            Get<IResourceReaderFactory>().GetResourceReader(null).ReturnsForAnyArgs(reader);
        }

        [TestMethod]
        public void Create_Group_From_Resource() => LucidTest
            .DefineExpected(() =>
            {
                const string Name = "file", Extension = ".json", Namespace = "My.Namespace", FullName = Name + Extension;
                return new
                {
                    Extension,
                    FullName,
                    Name,
                    Namespace,
                    ResourcePath = Namespace + "." + FullName
                };
            })
            .Arrange(param => { _resources = new[] { param.ResourcePath }; })
            .Act(GetResourceGroups)
            .Assert((expected, groups)=>
            {
                groups.ShouldNotBeNull();
                groups.Count().ShouldBe(1);

                var group = groups.First();
                group.ShouldNotBeNull();
                group.FullName.ShouldBe(expected.FullName);
                group.Extension.ShouldBe(expected.Extension);
                group.Name.ShouldBe(expected.Name);

                group.Tree.ShouldNotBeNull();
                group.Tree.FullName.ShouldBe(expected.ResourcePath);
                group.Tree.Name.ShouldBe(expected.FullName);
                group.Tree.Namespace.ShouldBe(expected.Namespace);
            });

        [TestMethod]
        public void Different_Files_In_Different_Groups() => LucidTest
            .DefineExpected(new
            {
                File1 = "Namespace.File1.json",
                File2 = "Namespace.File2.json"
            })
            .Arrange(param => { _resources = new[] { param.File1, param.File2 }; })
            .Act(GetResourceGroups)
            .Assert((expected, groups) =>
            {
                groups.Count().ShouldBe(2);
                groups.First().Tree.FullName.ShouldBe(expected.File1);
                groups.Skip(1).First().Tree.FullName.ShouldBe(expected.File2);
            });

        [TestMethod]
        public void The_Same_File_In_Two_Places_Result_In_Single_Group() => LucidTest
            .DefineExpected(() =>
            {
                const string FileName = "file.json";
                return new
                {
                    FileName,
                    Path1 = "Namespace." + FileName,
                    Path2 = "Namespace.More." + FileName
                };
            })
            .Arrange(param => { _resources = new[] { param.Path1, param.Path2 }; })
            .Act(GetResourceGroups)
            .Assert((expected, groups) =>
            {
                groups.Count().ShouldBe(1);
                var group = groups.First();
                group.FullName.ShouldBe(expected.FileName);
                group.Tree.Children.Count().ShouldBe(1);
            });

        private IEnumerable<IResourceGroup> GetResourceGroups() => Get<ResourceGroupFactory>().GetResourceGroups(null);
    }
}
