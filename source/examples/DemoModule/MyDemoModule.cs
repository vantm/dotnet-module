using DemoModule.Impl;
using Microsoft.Extensions.DependencyInjection;
using Modular;

namespace DemoModule;

public class MyDemoModule : ModuleBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IClock, SystemClock>();
        services.AddSingleton<IRandomUtil, DefaultRandomUtil>();
    }
}
