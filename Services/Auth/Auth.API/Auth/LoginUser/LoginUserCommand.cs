using Auth.API.DTOs;
using BuildingBlocks.CQRS;

namespace Auth.API.Auth.LoginUser;

public record LoginUserCommand(string Email, string Password) : ICommand<LoginUserResult>;

public record LoginUserResult(bool IsSuccess, string message = "");

public record LoginUserSuccededResult(Token Token, string RefreshToken) : LoginUserResult(true);