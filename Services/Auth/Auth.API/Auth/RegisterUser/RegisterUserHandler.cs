using Auth.API.Data.Models;
using BuildingBlocks.CQRS;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Auth.API.Auth.RegisterUser;

public class RegisterUserHandler(UserManager<AppUser> userManager) : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var appUser = request.Adapt<AppUser>();
        appUser.UserName = request.Email;
        try
        {
            IdentityResult identityResult = await userManager.CreateAsync(appUser, request.Password);

            StringBuilder messageBuilder = new();

            if (identityResult.Succeeded)
            {
                messageBuilder.Append("User created successfully");
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    messageBuilder.Append($"Error: {error.Code} {error.Description}");
                }
            }
            var registerUserResult = new RegisterUserResult(identityResult.Succeeded, messageBuilder.ToString());

            return registerUserResult;
        }
        catch (Exception e)
        {

            throw;
        }

    }
}
