using Auth.API.Data.Models;
using Auth.API.DTOs;
using Auth.API.Services.Token;
using BuildingBlocks.CQRS;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Auth.GetAccessTokenByRefreshToken;

public class GetAccessTokenByRefreshTokenHandler(
    UserManager<AppUser> userManager,
    ITokenHandler tokenHandler,
    IConfiguration configuration) : ICommandHandler<GetAccessTokenByRefreshTokenCommand, GetAccessTokenByRefreshTokenResponse>
{
    public async Task<GetAccessTokenByRefreshTokenResponse> Handle(GetAccessTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Where(x => x.RefreshToken == request.RefreshToken).FirstOrDefaultAsync();
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (user.RefreshTokenExpiryDate < DateTime.UtcNow)
        {
            throw new Exception("Token expiression has timed out");
        }
        Token token = tokenHandler.CreateAccessToken(int.Parse(configuration["JWT:TokenExpirationInMinutes"]!));
        return new GetAccessTokenByRefreshTokenResponse(token);
    }
}
