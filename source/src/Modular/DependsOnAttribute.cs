namespace Modular;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class DependsOnAttribute : Attribute
{
    public DependsOnAttribute(Type dependentModuleType, params Type[] dependentModuleTypes)
    {
        DependentModuleTypes = new[] { dependentModuleType }
            .Concat(dependentModuleTypes)
            .ToArray();

        foreach (var moduleType in DependentModuleTypes)
        {
            if (moduleType.IsAbstract)
            {
                throw new InvalidOperationException($"The module '{moduleType.FullName}' should not be an abstraction type");
            }
        }
    }

    public Type[] DependentModuleTypes { get; }
}
