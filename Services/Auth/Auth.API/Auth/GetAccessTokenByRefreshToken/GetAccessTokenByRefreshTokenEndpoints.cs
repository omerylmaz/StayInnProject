using BuildingBlocks.Endpoints;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Auth.GetAccessTokenByRefreshToken;

public class GetAccessTokenByRefreshTokenEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/refresh-token/{refreshToken}", async ([FromBody] GetAccessTokenByRefreshTokenCommand getAccessTokenByRefreshTokenCommand, [FromServices] ISender sender) =>
        {
            var response = await sender.Send(getAccessTokenByRefreshTokenCommand);
            return Results.Ok(response);
        });
    }
}
