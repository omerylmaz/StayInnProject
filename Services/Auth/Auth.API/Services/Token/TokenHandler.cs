
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Auth.API.Services.Token;

public class TokenHandler(IConfiguration configuration) : ITokenHandler
{
    public DTOs.Token CreateAccessToken(int minutes)
    {
        SigningCredentials signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"])), 
            SecurityAlgorithms.HmacSha256);

        DateTime accessTokenExpirationDate = DateTime.UtcNow.AddMinutes(minutes);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: configuration["JWT:Issuer"],
            audience: configuration["JWT:Audience"],
            notBefore: DateTime.UtcNow,
            expires: accessTokenExpirationDate,
            signingCredentials: signingCredentials
            );

        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        //string refreshToken = Guid.NewGuid().ToString();

        return new DTOs.Token(tokenString, accessTokenExpirationDate);
    }
}
