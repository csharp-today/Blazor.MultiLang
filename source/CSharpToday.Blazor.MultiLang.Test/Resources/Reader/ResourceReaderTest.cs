using CSharpToday.Blazor.MultiLang.Resources.Reader;
using LucidCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace CSharpToday.Blazor.MultiLang.Test.Resources.Reader
{
    [TestClass]
    public class ResourceReaderTest : BaseTest
    {
        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void Return_Null_When_Path_Is_Empty(string path) => LucidTest
            .Arrange(() => Get<ResourceReader>())
            .Act(reader => reader.GetResourceContext(path))
            .Assert(content => content.ShouldBeNull());
    }
}
