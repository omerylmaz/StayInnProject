namespace Auth.API.DTOs.LoginUser
{
    public record LoginUserResponse(bool IsSuccess, string message = "");

    public record LoginUserSucceededResponse(Token Token) : LoginUserResponse(true);
}
