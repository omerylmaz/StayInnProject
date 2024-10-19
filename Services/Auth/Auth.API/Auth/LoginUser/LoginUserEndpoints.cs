using Auth.API.Auth.RegisterUser;
using BuildingBlocks.Endpoints;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Auth.LoginUser;

public class LoginUserEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async ([FromBody] LoginUserCommand loginUserCommand, [FromServices] ISender sender) =>
        {
            LoginUserResult result = await sender.Send(loginUserCommand);
            if (!result.IsSuccess)
            {
                return Results.Unauthorized();
            }
            return Results.Ok(result);
        });
    }
}
