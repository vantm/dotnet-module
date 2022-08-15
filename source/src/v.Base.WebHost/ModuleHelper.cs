using v.Base.Core;
using System.Reflection;

namespace v.Base.WebHost;

internal static class ModuleHelper
{
    public static Type[] GetDependentModuleTypes(IEnumerable<Type> fromTypes)
    {
        var queue = new Queue<Type>(fromTypes);
        var output = new HashSet<Type>();

        while (queue.Count > 0)
        {
            var currentModuleType = queue.Dequeue();

            output.Add(currentModuleType);

            var dependentModuleTypes = GetDependentModuleTypes(currentModuleType);

            foreach (var dependentModuleType in dependentModuleTypes)
            {
                if (!queue.Contains(dependentModuleType) &&
                    !output.Contains(dependentModuleType))
                {
                    queue.Enqueue(dependentModuleType);
                }
            }
        }

        return output.Reverse().ToArray();
    }

    private static Type[] GetDependentModuleTypes(Type currentModuleType)
    {
        var dependsOnAttribute = currentModuleType.GetCustomAttribute<DependsOnAttribute>();

        if (dependsOnAttribute == null)
        {
            return Array.Empty<Type>();
        }

        return dependsOnAttribute.DependentModuleTypes;
    }
}
