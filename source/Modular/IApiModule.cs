using Microsoft.AspNetCore.Routing;

namespace Modular;

public interface IApiModule : IModule
{
    void MapEndpoints(IEndpointRouteBuilder routeBuilder);
}
