using BuildingBlocks.CQRS;
using MediatR;

namespace Auth.API.Auth.RegisterUser;

public record RegisterUserCommand(string FullName, string Email, string PhoneNumber, string Password, string RoleType) : ICommand<RegisterUserResult>;

public record RegisterUserResult(bool IsSuccess, string Message);