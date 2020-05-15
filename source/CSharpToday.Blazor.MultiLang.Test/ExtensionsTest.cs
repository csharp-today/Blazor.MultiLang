using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpToday.Blazor.MultiLang.Test
{
    [TestClass]
    public class ExtensionsTest : BaseExtensionTest
    {
        protected override void AddServices(IServiceCollection services) => services.AddBlazorMultiLang();

        [TestMethod]
        public void Create_IMultiLang() => Validate<IMultiLang>();
    }
}
