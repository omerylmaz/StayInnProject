using Auth.API.Data.Models;
using BuildingBlocks.CQRS;
using Microsoft.AspNetCore.Identity;
using Google.Apis.Auth;
using Auth.API.DTOs;
using Auth.API.Services.Token;
using MediatR;

namespace Auth.API.Auth.GoogleLogin;

public class GoogleLoginHandler(
    UserManager<AppUser> userManager,
    ITokenHandler tokenHandler,
    IConfiguration configuration)
    : ICommandHandler<GoogleLoginCommand, GoogleLoginResult>
{
    public async Task<GoogleLoginResult> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { configuration["ExternalLoginServices:Google:ClientId"] }
        };

        var payload = await ValidateUser(request.IdToken);

        var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);

        var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);


        if (user is null)
        {
            user = await userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                user = await CreateUserAndLogin(request);
            }
            var loginResult = await userManager.AddLoginAsync(user, info);
            if (!loginResult.Succeeded)
                throw new Exception("Failed to add external login.");
        }

        Token token = tokenHandler.CreateAccessToken(int.Parse(configuration["JWT:TokenExpirationInMinutes"]!));

        return new(token, user.RefreshToken);
    }

    private async Task<GoogleJsonWebSignature.Payload> ValidateUser(string IdToken)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings()
        {
            Audience = new List<string> { configuration["ExternalLoginServices:Google:ClientId"] }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(IdToken, settings);

        return payload;
    }

    private async Task<AppUser> CreateUserAndLogin(GoogleLoginCommand request)
    {
        var user = new AppUser()
        {
            Email = request.Email,
            Id = Guid.NewGuid().ToString(),
            FullName = request.Name,
            UserName = request.Email,
            RefreshToken = Guid.NewGuid().ToString(),
            RefreshTokenExpiryDate = DateTime.UtcNow.AddMinutes(int.Parse(configuration["JWT:RefreshTokenExpirationInMinutes"]!)),
        };
        var identityResult = await userManager.CreateAsync(user);

        if (!identityResult.Succeeded)
            throw new Exception("Failed to create user.");

        return user;
    }
}
