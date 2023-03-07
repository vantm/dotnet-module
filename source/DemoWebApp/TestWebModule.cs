using Modular;

namespace DemoWebApp;

[DependsOn(typeof(ModuleB), typeof(ModuleF))]
public class TestWebModule : IApiModule
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment) { }

    public void MapEndpoints(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("/", () => "The test web module works")
            .ExcludeFromDescription();

        routeBuilder.MapGet("/hello", () => "Hello World!");
    }
}
