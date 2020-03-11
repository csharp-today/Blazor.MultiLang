using System.Collections.Generic;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang.Resources
{
    internal class ResourceReader : IResourceReader
    {
        public IEnumerable<string> GetResources(Assembly assembly = null)
        {
            if (assembly is null)
            {
                assembly = Assembly.GetExecutingAssembly();
            }

            return assembly.GetManifestResourceNames();
        }
    }
}
