using Microsoft.AspNetCore.Routing;

namespace BuildingBlocks.Endpoints;

public interface IEndpointModule
{
    void AddRoutes(IEndpointRouteBuilder app);
}
