using Auth.API.DTOs;
using BuildingBlocks.CQRS;

namespace Auth.API.Auth.GoogleLogin;

using Newtonsoft.Json;
using System.Text.Json.Serialization;

public record GoogleLoginCommand(
    [property: JsonPropertyName("token")] string IdToken,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("given_name")] string GivenName,
    [property: JsonPropertyName("family_name")] string FamilyName,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("picture")] string Picture,
    [property: JsonPropertyName("iss")] string Provider
) : ICommand<GoogleLoginResult>;


public record GoogleLoginResult(Token Token, string RefreshToken);
