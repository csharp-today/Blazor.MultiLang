using System.Collections.Generic;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang.Resources
{
    internal class ResourceReader : IResourceReader
    {
        public IEnumerable<string> GetResources(Assembly assembly)
        {
            return assembly.GetManifestResourceNames();
        }
    }
}
