using v.Base.Core;

namespace DemoWebApp;

public class SomeOtherModule : ModuleBase
{
    public override Task InitAsync(IServiceProvider services, CancellationToken cancellationToken = default)
    {
        var logger = services.GetRequiredService<ILogger<SomeOtherModule>>();

        logger.LogDebug("Module had been initialized");

        return Task.CompletedTask;
    }
}
