using BuildingBlocks.Endpoints;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Auth.RegisterUser;

public class RegisterUserEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/register", async ([FromBody] RegisterUserCommand registerUserCommand, [FromServices] ISender sender) => 
        {
            var response = await sender.Send(registerUserCommand);

            return Results.Created("/register", response);
        });
    }
}
