using Modular;
using System.Reflection;

namespace Modular;

internal static class ModuleHelper
{
    public static Type[] GetDependentModuleTypes(IEnumerable<Type> fromTypes)
    {
        var queue = new Queue<Type>(fromTypes);
        var output = new HashSet<Type>();

        while (queue.Count > 0)
        {
            var currentModuleType = queue.Dequeue();
            var dependentModuleTypes = GetDependentModuleTypes(currentModuleType);
            var remainDependentModuleTypes = dependentModuleTypes.Where(x => !output.Contains(x)).ToArray();

            if (remainDependentModuleTypes.Length == 0)
            {
                output.Add(currentModuleType);
            }
            else
            {
                foreach (var dependentModuleType in remainDependentModuleTypes.Where(x => !queue.Contains(x)))
                {
                    queue.Enqueue(dependentModuleType);
                }

                queue.Enqueue(currentModuleType);
            }
        }

        return output.ToArray();
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
