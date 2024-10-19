using Auth.API.Data.Models;
using Auth.API.DTOs;
using Auth.API.Exceptions;
using Auth.API.Services.Token;
using BuildingBlocks.CQRS;
using Microsoft.AspNetCore.Identity;

namespace Auth.API.Auth.LoginUser;

public class LoginUserHandler(
    UserManager<AppUser> userManager, 
    SignInManager<AppUser> signInManager,
    ITokenHandler tokenHandler,
    IConfiguration configuration) : ICommandHandler<LoginUserCommand, LoginUserResult>
{
    public async Task<LoginUserResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return new LoginUserResult(false, "Username or password wrong");
        }

        SignInResult signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (signInResult.Succeeded)
        {
            Token token = tokenHandler.CreateAccessToken(int.Parse(configuration["JWT:TokenExpirationInMinutes"]!));
            user.RefreshToken = Guid.NewGuid().ToString();
            user.RefreshTokenExpiryDate = DateTime.UtcNow.AddMinutes(int.Parse(configuration["JWT:RefreshTokenExpirationInMinutes"]!));
            await userManager.UpdateAsync(user);
            return new LoginUserSuccededResult(token, user.RefreshToken);
        }

        else
        {
            return new LoginUserResult(false, "Username or password wrong");
        }
    }
}