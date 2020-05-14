using System.Collections.Generic;
using System.Linq;

namespace CSharpToday.Blazor.MultiLang.Resources.Value
{
    internal class CascadingValueProvider : IValueProvider
    {
        private readonly IEnumerable<IValueProvider> _providers;

        public CascadingValueProvider(IEnumerable<IValueProvider> providers) => _providers = providers;

        public CascadingValueProvider(params IValueProvider[] providers)
            : this(providers.AsEnumerable()) { }

        public string GetValue(string key)
        {
            foreach (var provider in _providers)
            {
                var value = provider.GetValue(key);
                if (value is { })
                {
                    return value;
                }
            }

            return null;
        }
    }
}
