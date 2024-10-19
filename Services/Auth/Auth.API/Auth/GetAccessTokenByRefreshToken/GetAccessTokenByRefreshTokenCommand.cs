using Auth.API.DTOs;
using BuildingBlocks.CQRS;
using System.Windows.Input;

namespace Auth.API.Auth.GetAccessTokenByRefreshToken;

public record GetAccessTokenByRefreshTokenCommand(string RefreshToken) : ICommand<GetAccessTokenByRefreshTokenResponse>;

public record GetAccessTokenByRefreshTokenResponse(Token Token);