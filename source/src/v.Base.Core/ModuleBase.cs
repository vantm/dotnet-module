using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace v.Base.Core;

public abstract class ModuleBase : IModule
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public IConfiguration Configuration { get; private set; }
    public IHostEnvironment Environment { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public virtual void ConfigureServices(IServiceCollection services)
    {
        // intentionally left blank
    }

    public virtual Task InitAsync(IServiceProvider services, CancellationToken cancellationToken = default)
    {
        // intentionally left blank
        return Task.CompletedTask;
    }
}
