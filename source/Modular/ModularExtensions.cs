using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using System.Reflection;
using System.Linq;

namespace Modular;

public static class ModularExtensions
{
    public static void AddModules(this WebApplicationBuilder builder)
    {
        builder.AddModules(Assembly.GetEntryAssembly()!);
    }

    public static void AddModules(this WebApplicationBuilder builder, Type markedType)
    {
        builder.AddModules(markedType.Assembly);
    }

    public static void AddModules(this WebApplicationBuilder builder, Assembly scanAssembly)
    {
        var assemblyModules = scanAssembly.GetTypes()
            .Where(x => !x.IsAbstract && x.IsClass && x.IsAssignableTo(typeof(IModule)))
            .ToArray();

        var moduleTypes = ModuleHelper.GetDependentModuleTypes(assemblyModules);
        var moduleAssemblies = moduleTypes.Select(x => x.Assembly).Distinct().ToArray();

        var modules = new IModule[moduleTypes.Length];
        var index = 0;

        foreach (var moduleType in moduleTypes)
        {
            var module = (IModule)Activator.CreateInstance(moduleType)!;

            modules[index++] = module;

            module.ConfigureServices(builder.Services, builder.Configuration, builder.Environment);
        }

        builder.Services.AddSingleton<IEnumerable<IModule>>(_ => modules);
    }

    public static void MapModuleEndpoints(this WebApplication app)
    {
        var modules = app.Services.GetRequiredService<IEnumerable<IModule>>();

        foreach (var module in modules.Where(x => x is IApiModule))
        {
            ((IApiModule)module).MapEndpoints(app);
        }
    }
}