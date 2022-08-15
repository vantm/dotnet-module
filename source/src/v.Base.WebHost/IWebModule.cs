using v.Base.Core;
using Microsoft.AspNetCore.Builder;

namespace v.Base.WebHost;

public interface IWebModule : IModule
{
    void ConfigureBuilder(WebApplicationBuilder builder);
    void PreConfigureApp(WebApplication app);
    void ConfigureApp(WebApplication app);
    void PostConfigureApp(WebApplication app);
}
