using Modular;
using Microsoft.AspNetCore.Builder;

namespace Modular.WebHost;

public abstract class WebModuleBase : ModuleBase, IWebModule
{
    public virtual void ConfigureBuilder(WebApplicationBuilder builder)
    {
        // intentionally left blank
    }

    public virtual void PostConfigureApp(WebApplication app)
    {
        // intentionally left blank
    }

    public virtual void ConfigureApp(WebApplication app)
    {
        // intentionally left blank
    }

    public virtual void PreConfigureApp(WebApplication app)
    {
        // intentionally left blank
    }
}
