using v.Base.Core;
using v.Base.Modules.OS.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace v.Base.Modules.OS;

public class OSModule : ModuleBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ISystemClock, DefaultSystemClock>();
    }
}
