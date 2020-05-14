using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang.Resources.Reader
{
    internal class ResourceReader : IResourceReader
    {
        private readonly Assembly _assembly;

        public ResourceReader(Assembly assembly) => _assembly = assembly;

        public string GetResourceContext(string resourcePath)
        {
            using var stream = _assembly.GetManifestResourceStream(resourcePath);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public IEnumerable<string> GetResources() => _assembly.GetManifestResourceNames();
    }
}
