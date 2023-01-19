using DemoModule;
using Modular;

namespace DemoWebApp;

[DependsOn(
    typeof(MyDemoModule)
)]
public class SomeOtherModule : ModuleBase
{
    public override Task InitAsync(IServiceProvider services, CancellationToken cancellationToken = default)
    {
        var logger = services.GetRequiredService<ILogger<SomeOtherModule>>();
        var rand = services.GetRequiredService<IRandomUtil>();
        var clock = services.GetRequiredService<IClock>();

        logger.LogDebug("Module had been initialized. Time is {time} and random number is {number}.", clock.Now, rand.Next(1000));

        return Task.CompletedTask;
    }
}
