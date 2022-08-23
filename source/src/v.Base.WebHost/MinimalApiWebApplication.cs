using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using v.Base.Core;

namespace v.Base.WebHost;

public sealed class MinimalApiWebApplication
{
    private readonly WebApplicationBuilder _builder;
    private readonly List<Type> _registeredModuleTypes = new();

    public TimeSpan MaximumModuleInitializationTimeout { get; set; } = TimeSpan.FromMinutes(3);
    public bool HasSwagger { get; set; } = true;
    public bool HasSwaggerOnProduction { get; set; } = false;
    public RequestDelegate? CustomExceptionHandler { get; set; }
    public BindingFlags BindingFlags { get; private set; }

    public MinimalApiWebApplication(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);
    }

    public MinimalApiWebApplication AddModule<T>() where T : class, IModule, new()
    {
        var moduleType = typeof(T);

        if (moduleType.IsAbstract)
        {
            throw new InvalidOperationException($"The module '{moduleType.FullName}' should not be an abstraction type");
        }

        _registeredModuleTypes.Add(moduleType);

        return this;
    }

    private IEnumerable<IModule> CreateModuleInstances(Type[] moduleTypes)
    {
        var modules = new IModule[moduleTypes.Length];
        var index = 0;

        foreach (var moduleType in moduleTypes)
        {
            var module = (IModule)Activator.CreateInstance(moduleType)!;

            var propertyInfos = moduleType.GetProperties().Where(x => x.CanWrite).ToArray();

            propertyInfos.FirstOrDefault(x => x.Name == "Configuration")?.SetValue(module, _builder.Configuration);
            propertyInfos.FirstOrDefault(x => x.Name == "Environment")?.SetValue(module, _builder.Environment);

            modules[index++] = module;
        }

        return modules;
    }

    public async Task RunAsync(string? url = null)
    {
        if (HasSwagger)
        {
            _builder.Services.AddEndpointsApiExplorer();
            _builder.Services.AddSwaggerGen(o =>
            {
                o.CustomSchemaIds(t => t.FullName?.Replace("+", ".") ?? t.Name);
            });
        }

        var moduleTypes = ModuleHelper.GetDependentModuleTypes(_registeredModuleTypes);
        var moduleAssemblies = moduleTypes.Select(x => x.Assembly).Distinct().ToArray();

        _builder.Services.AddValidatorsFromAssemblies(moduleAssemblies);
        _builder.Services.AddAutoMapper(moduleAssemblies);

        var modules = CreateModuleInstances(moduleTypes);

        foreach (var module in modules)
        {
            if (module is IWebModule webModule)
            {
                webModule.ConfigureBuilder(_builder);
            }

            module.ConfigureServices(_builder.Services);
        }

        using var app = _builder.Build();

        using var source = new CancellationTokenSource(MaximumModuleInitializationTimeout);

        await using (var scope = app.Services.CreateAsyncScope())
        {
            await Task.WhenAll(modules.Select(x => x.InitAsync(scope.ServiceProvider, source.Token)));
        }

        var webModules = modules.Where(x => x is IWebModule webModule).Cast<IWebModule>();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        foreach (var webModule in webModules)
        {
            webModule.PreConfigureApp(app);
        }

        if (HasSwagger && (HasSwaggerOnProduction || app.Environment.IsDevelopment()))
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler(exceptionHandlerAppBuilder =>
        {
            if (CustomExceptionHandler != null)
            {
                exceptionHandlerAppBuilder.Run(CustomExceptionHandler);
            }
            else
            {
                var logger = exceptionHandlerAppBuilder.ApplicationServices.GetRequiredService<ILogger<MinimalApiWebApplication>>();
                exceptionHandlerAppBuilder.Run(ctx => ValidationExceptionHandlingMiddleware.Handle(ctx, logger));
            }
        });

        foreach (var webModule in webModules)
        {
            webModule.ConfigureApp(app);
        }

        foreach (var webModule in webModules)
        {
            webModule.PostConfigureApp(app);
        }

        await app.RunAsync(url);
    }

    public static MinimalApiWebApplication Create(string[] args) => new(args);
}
