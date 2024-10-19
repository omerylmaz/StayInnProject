using BuildingBlocks.Endpoints;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Auth.GoogleLogin;

public class GoogleLoginEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("google-login", async ([FromBody] GoogleLoginCommand googleLoginCommand, [FromServices] ISender sender) =>
        {
            var response = await sender.Send(googleLoginCommand);

            return Results.Ok(response);
        });
    }
}
