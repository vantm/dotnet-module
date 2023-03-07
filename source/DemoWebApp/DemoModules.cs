using Modular;
using SomeModules;
using System.Diagnostics;

namespace DemoWebApp;

[DependsOn(typeof(ModuleC), typeof(ModuleD))]
public class ModuleB : IModule
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddHostedService<MockHostedService>();
    }
}

public class MockHostedService : IHostedService
{
    private readonly ILogger<MockHostedService> _logger;
    private readonly IWebHostEnvironment _env;
    private readonly IEnumerable<IModule> _allModules;

    public MockHostedService(ILogger<MockHostedService> logger, IWebHostEnvironment env, IEnumerable<IModule> allModules)
    {
        _logger = logger;
        _env = env;
        _allModules = allModules;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Mock Hosted Service running.");


        if (_env.IsDevelopment())
        {
            var stackTrace = new StackTrace();
            var logName = stackTrace.GetFrame(1)!.GetMethod()!.Name;
            var moduleNames = _allModules.Select(x => x.GetType().Name);
            _logger.LogDebug("Ordered modules: {0}", string.Join(", ", moduleNames));
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Mock Service is stopping.");
        return Task.CompletedTask;
    }
}

public abstract class NamedModule : IModule
{
    public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
    }
}

public class ModuleF : NamedModule { }

[DependsOn(typeof(ModuleF), typeof(ModuleH))]
public class ModuleG : NamedModule { }