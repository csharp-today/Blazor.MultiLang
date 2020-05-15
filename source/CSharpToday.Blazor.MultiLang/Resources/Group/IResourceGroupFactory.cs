using System.Collections.Generic;
using System.Reflection;

namespace CSharpToday.Blazor.MultiLang.Resources.Group
{
    internal interface IResourceGroupFactory
    {
        IEnumerable<IResourceGroup> GetResourceGroups(Assembly assembly);
    }
}
