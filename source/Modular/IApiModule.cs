using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Routing;

namespace Modular;

public interface IApiModule : IModule
{
    void MapEndpoints(IEndpointRouteBuilder routeBuilder);
}
