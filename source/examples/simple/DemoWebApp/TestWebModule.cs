using Modular;
using Modular.WebHost;

namespace DemoWebApp;

[DependsOn(
    typeof(SomeOtherModule)
)]
public class TestWebModule : WebModuleBase
{
    public override Task InitAsync(IServiceProvider services, CancellationToken cancellationToken = default)
    {
        var logger = services.GetRequiredService<ILogger<TestWebModule>>();

        logger.LogDebug("Module had been initialized");

        return Task.CompletedTask;
    }

    public override void ConfigureApp(WebApplication app)
    {
        app.MapGet("/", () => "The test web module works")
            .ExcludeFromDescription();

        app.MapGet("/hello", () => "Hello World!");
    }
}
