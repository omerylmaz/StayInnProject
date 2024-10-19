namespace Auth.API.Services.Token;

public interface ITokenHandler
{
    DTOs.Token CreateAccessToken(int minutes);
}
